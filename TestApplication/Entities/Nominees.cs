using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class Nominees
    {
        public string[] Id { get; set; }
        public string[] Relation { get; set; }
        public string[] Name { get; set; }
        public string[] Address { get; set; }
        public int MemberId { get; set; }
    }
}