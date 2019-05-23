using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApplication.Models;

namespace TestApplication.Entities
{
    public class ShareHolding
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Member")]
        public int MemberId { get; set; }

        [Display(Name = "Share")]
        public int Quantity { get; set; }

        [Display(Name = "Promoter")]
        public int ReferId { get; set; }

        [Display(Name = "Price Per Share")]
        public decimal PricePerShare { get; set; }

        [Display(Name = "Transfer Date")]
        public DateTime TransferDate { get; set; } = DateTime.Now;
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Member Member { get; set; }

        [NotMapped]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public decimal CalculatedTotalPrice => PricePerShare * Quantity;

        public string MemberName => MemberModel.GetMemberNameById(MemberId);
        public string ReferName => MemberModel.GetMemberNameById(ReferId);

        public string TransferDateString => TransferDate.ToShortDateString();
    }
}