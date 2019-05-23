using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TestApplication.DataLayer;
using TestApplication.Helpers;
using static TestApplication.Helpers.Enumerations;

namespace TestApplication.Entities
{
    public class MemberAccount
    {
        public MemberAccount()
        {
            AmountTransaction = new AmountTransaction();
            AmountTransactions = new HashSet<AmountTransaction>();
        }
        [Key]
        public int Id { get; set; }

        [Display(Name = "Agent/Advisor")]
        public int? AgentId { get; set; }

        [Display(Name = "Branch")]
        public int? BranchId { get; set; }

        [Display(Name = "Member")]
        public int MemberId { get; set; }

        [Display(Name = "Scheme")]
        public int? SchemeId { get; set; }

        [Display(Name = "Open Date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Closing Date")]
        public DateTime? ClosingDate { get; set; }

        public int? AmountTransactionId { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Joint Account Member")]
        public int? JointAccountMemberId { get; set; }

        [Display(Name = "Nominee")]
        public bool IsNominee { get; set; }

        public int AccountTypeId { get; set; }

        [Display(Name = "Amount Deposit")]
        public decimal MinAmount { get; set; }

        [Display(Name = "Reason to Close A/c")]
        public string CloseAccountReason { get; set; }

        public bool MinAmountCheckbox { get; set; }

        public int AgentCollectionId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Scheme Scheme { get; set; }
        public virtual Branches Branches { get; set; }

        [InverseProperty("MemberAccount")]
        public virtual ICollection<AmountTransaction> AmountTransactions { get; set; }

        public virtual ICollection<FdAccount> FdAccounts { get; set; }

        //Not a part of entity
        [NotMapped]
        public AmountTransaction AmountTransaction { get; set; }

        [NotMapped]
        [Display(Name = "Interest Accrued")]
        public decimal InterestAccrued { get; set; }    // will write logic later by asking the client

        [NotMapped]
        public decimal? AmountWithInterest
        {
            get => TotalBalance + InterestAccrued;
            set => AmountTransaction.Amount = value;
        }

        [NotMapped]
        public string ClosingDateText => ClosingDate?.ToShortDateString() ?? string.Empty;

        [NotMapped]
        public string StatusText => ((MemberAccountStatus)StatusId).GetDescription();
        

        [NotMapped]
        [Display(Name = "Account Type")]
        public bool IsJointAccount => JointAccountMemberId.HasValue;

        [NotMapped]
        [Display(Name = "Total Balance")]
        public decimal? TotalBalance
        {
            get
            {
                var transactions = Repository.Instance.All<AmountTransaction>().Where(x => x.MemberAccountId == Id && x.StatusId == (int)AmountTransactionStatus.Approved).ToList();
                return transactions.Where(x => x.TransactionType == (int)TransactionType.Credit)?.Sum(y => y.Amount) ?? 0 -
                       transactions.Where(x => x.TransactionType == (int)TransactionType.Debit)?.Sum(y => y.Amount) ?? 0;
            }
        }


    }
}