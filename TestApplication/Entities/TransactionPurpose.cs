using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class TransactionPurpose
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Charge Type")]
        public string Name { get; set; }
        
        [DataType(DataType.Currency)]
        [Display(Name="Amount")]
        public decimal? Charges { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}