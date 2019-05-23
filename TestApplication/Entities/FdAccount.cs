using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class FdAccount
    {
        [Key]
        public int Id { get; set; }
        public int MemberAccountId { get; set; }

        [Display(Name ="Fd Amount")]
        public decimal FdAmount { get; set; }
        public bool IsLessMinAmount { get; set; }
        public bool DeductAmountFromSavingAccount { get; set; }
        public bool TdsDeduction { get; set; }
        public int? JointAccountMemberId { get; set; }
        public bool IsNominee { get; set; }
        public bool RenewAfterMaturity { get; set; }
        public bool PayFromSavingAccount { get; set; }
        public bool PenaltyCharges { get; set; }

        [Display(Name = "Open Date")]
        public DateTime? OpenDate { get; set; }

        [Display(Name ="Closing Date")]
        public DateTime? ClosingDate { get; set; }
        public DateTime CreationDate { get; set; }

        [Display(Name = "Scheme")]
        public int SchemeId { get; set; }

        public virtual Scheme Scheme { get; set; }
        public virtual MemberAccount FdMemberAccount { get; set; }


        [NotMapped]
        [Display(Name = "Account Type")]
        public bool IsJointAccount => JointAccountMemberId.HasValue;
        [NotMapped]
        public string CreationDateString => OpenDate.HasValue ? OpenDate.Value.ToShortDateString() : string.Empty;
        [NotMapped]
        public string ClosingDateString => ClosingDate.HasValue ? ClosingDate.Value.ToShortDateString() : string.Empty;
    }
}