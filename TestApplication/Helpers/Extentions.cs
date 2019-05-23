using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TestApplication.DataLayer;

namespace TestApplication.Helpers
{
    public class Pagination<T>
    {
        IQueryable<T> Query { get; set; }
        IQueryable<T> PageOptions { get; set; }
    }

    internal static class Extensions
    {
        public static IQueryable<T> OrderByDynamic<T>(
           this IQueryable<T> query,
           string orderByMember,
           bool desc)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));

            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);

            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                desc ? "OrderByDescending" : "OrderBy",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(orderBy);
        }

        public static IQueryable<T> Pagination<T>(this IQueryable<T> query, PageOptions options)
        {
            if (options == null)
                options = PageOptions.Default;

            if (string.IsNullOrWhiteSpace(options.OrderBy))
            {
                options.OrderBy = "Id";
                options.Descending = false;
            }
            query = query.OrderByDynamic(options.OrderBy, options.Descending);
            query = query.Skip(options.GetOffset()).Take(options.Limit);
            return query;
        }

        
        public static IPager<T> ToPager<T>(this IQueryable<T> query, int count, PageOptions pageOptions)
        {
            return new Pager<T>(count, pageOptions, query);
        }
    }
}