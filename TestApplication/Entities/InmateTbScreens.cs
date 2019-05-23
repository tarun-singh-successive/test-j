using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class InmateTbScreens
    {
        public int Id { get; set; }
        public int InmateId { get; set; }
        public DateTime CreationDate { get; set; }
        public string PpdApplied { get; set; }
        public string Comment { get; set; }
        public string CreationDateString => CreationDate.ToString("MM-dd-yyyy HH:mm");
    }
}