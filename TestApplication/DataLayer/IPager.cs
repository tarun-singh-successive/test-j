using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.DataLayer
{

    public interface IPager
    {
        int TotalRecords { get; set; }
        int CurrentPage { get; set; }
        int RecordsPerPage { get; set; }
        int TotalPages { get; }
        bool AppendLimitToQuery { get; set; }
    }

    public interface IPager<T> : IPager
    {
        IEnumerable<T> Records { get; set; }
    }

    public class Pager<T> : IPager<T>
    {
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int RecordsPerPage { get; set; }
        public bool AppendLimitToQuery { get; set; }
        public int TotalPages
        {
            get
            {
                return ((TotalRecords - 1) / RecordsPerPage) + 1;
            }
        }
        public IEnumerable<T> Records { get; set; }

        public Pager()
        {

        }

        public Pager(int totalRecords, Helpers.PageOptions pageOptions, IEnumerable<T> records)
        {
            TotalRecords = totalRecords;
            CurrentPage = pageOptions.Page;
            RecordsPerPage = pageOptions.Limit;
            Records = records;
        }
    }
}