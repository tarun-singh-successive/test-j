using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.DataLayer
{
    public class PagedView<T> where T : class
    {
        public string FilterKey { get; set; }
        public int RecordIndex { get; set; }
        public int TotalRecords { get; set; }
        public int? Page { get; set; }
        public string Sort { get; set; }
        public string SortDir { get; set; }
        public IEnumerable<T> ResultSet { get; set; }
    }
}