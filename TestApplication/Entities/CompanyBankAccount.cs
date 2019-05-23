using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class CompanyBankAccount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public string IfscCode { get; set; }

        [Display(Name ="Open Date")]
        public DateTime? OpenDate { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public string OpenDateString => OpenDate?.ToShortDateString() ?? string.Empty;
    }
}