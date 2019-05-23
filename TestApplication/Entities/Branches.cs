using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class Branches
    {
        public Branches()
        {
            //States = new HashSet<State>();
        }

        [Key]
        public int Id { get; set; }

        [Display(Name ="Branch")]
        public string Name { get; set; }
        public int StateId { get; set; }
        public string City { get; set; }
        public DateTime? OpeningDate { get; set; }
        public virtual State State { get; set; }
        public string OpeningDateString => OpeningDate?.ToShortDateString() ?? string.Empty;

    }
}