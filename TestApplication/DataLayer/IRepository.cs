using System;
using System.Linq;
using System.Linq.Expressions;

namespace TestApplication.DataLayer
{
    interface IRepository
    {
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        IQueryable<T> All<T>() where T : class;

        /// <summary>
        ///  Gets all objects from database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index">pecified the page index.</param>
        /// <param name="size">Specified the page size.</param>
        /// <returns></returns>
        IQueryable<T> All<T>(int index, int size) where T : class;


        ///// <summary>
        ///// Gets objects from database by filter.
        ///// </summary>
        ///// <param name="predicate">Specified a filter</param>
        //IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate)where T:class;


        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate, params string[] includes) where T : class;

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        IQueryable<T> Filter<T>(Expression<Func<T, bool>> filter,
            out int total, int index = 0, int size = 50) where T : class;

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        T Find<T>(params object[] keys) where T : class;

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        T Find<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        T Insert<T>(T t, bool commit) where T : class;

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>        
        int Delete<T>(T t) where T : class;

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        int Delete<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        int Update<T>(T t) where T : class;

        /// <summary>
        /// Get the total objects count.
        /// </summary>
        int Count<T>() where T : class;
    }
}
