using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class Agent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int MemberId { get; set; }
    }
}