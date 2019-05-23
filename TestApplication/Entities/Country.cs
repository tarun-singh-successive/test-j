using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string SortName { get; set; }
        public int PhoneCode { get; set; }
        public string Name { get; set; }
    }
}