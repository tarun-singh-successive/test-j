using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.Helpers
{
    public class PageOptions
    {
        public const int DefaultPageLimit = 25;
        public const int DefaultFundsLimit = 100;

        private int _page = 1;
        /// <summary>
        /// Current page number. Default value is 1.
        /// </summary>
        public int Page
        {
            get { return _page; }
            set
            {
                if (_page < 1) throw new Exception("Page should not be 0 or negative value.");
                _page = value;
            }
        }

        private int _limit = DefaultPageLimit;
        /// <summary>
        /// Number of records to fetch. Default value is 20.
        /// </summary>
        public int Limit
        {
            get { return _limit; }
            set
            {
                if (_limit < 1) throw new Exception("Limit should not be 0 or negative value.");
                _limit = value;
            }
        }

        public string OrderBy { get; set; }
        public bool Descending { get; set; }
        public int GetOffset()
        {
            return (Math.Max(Page, 1) - 1) * Limit;
        }

        public PageOptions(int page)
            : this(page, DefaultPageLimit, null, false)
        {

        }

        public PageOptions(int page, int limit)
           : this(page, limit, null, false)
        {

        }

        public PageOptions(int page, string orderby, bool desc)
            : this(page, DefaultPageLimit, orderby, desc)
        {
        }

        public PageOptions(int page, int limit, string orderby, bool desc)
        {
            Page = page < 1 ? 1 : page;
            OrderBy = orderby;
            Limit = limit;
            Descending = desc;
        }

        public static PageOptions Default = new PageOptions(1, "Id", true)
        {
            Limit = DefaultPageLimit
        };

    }
}