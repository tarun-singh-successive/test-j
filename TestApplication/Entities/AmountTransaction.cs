using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TestApplication.DataLayer;
using TestApplication.Helpers;
using static TestApplication.Helpers.Enumerations;

namespace TestApplication.Entities
{
    public class AmountTransaction
    {

        public AmountTransaction()
        {

        }
        [Display(Name ="Reference Id")]
        [Key]
        public string Id { get; set; }

        [ForeignKey("MemberAccount")]
        public int MemberAccountId { get; set; }

        [Display(Name = "Bank Name")]
        public int? ChequeBankId { get; set; }

        [Display(Name = "Cheque Date")]
        public DateTime? ChequeDate { get; set; }

        [Display(Name = "Cheque No")]
        public int? ChequeNo { get; set; }

        [Display(Name = "Transaction Type")]
        public int TransactionType { get; set; }

        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [Display(Name ="Created At")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Currency)]
        [Range(1.00, int.MaxValue, ErrorMessage = "Please enter an Amount greater than 0")]
        public decimal? Amount { get; set; }

        [Required]
        [Display(Name = "Payment Mode")]
        public int PaymentModeId { get; set; }

        [Display(Name = "Transaction Mode")]
        public int? OnlineTransferModeId { get; set; }

        [Display(Name = "UTR/ Transaction No")]
        public string OnlineTransferId { get; set; }

        [Display(Name = "Credited in Company Account")]
        public bool CreditInCompany { get; set; }

        [Display(Name = "Remarks (if any)")]
        public string Remarks { get; set; }

        [Display(Name = "Transfer Date")]
        public DateTime? OnlineTransferDate { get; set; }


        [Display(Name = "Charge Type")]
        public int? TransactionPurposeId { get; set; }

        [Display(Name ="TransactonStatus")]
        public int StatusId { get; set; }

        public bool IsDelete { get; set; }

        public int? CompanyBankAccountId { get; set; }

        public DateTime? ChequeClearingDate { get; set; }

        public string ApprovedBy { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public string ApprovedNote { get; set; }

        public DateTime LastModificationDateTime { get; set; } = DateTime.Now;

        public virtual MemberAccount MemberAccount { get; set; }

        public virtual TransactionPurpose TransactionPurpose { get; set; }


        [NotMapped]
        public string PanNo { get; set; }
        [NotMapped]
        public string TransactionDateString => TransactionDate.ToShortDateString();
        [NotMapped]
        public string TransactionTypeString => TransactionType == (int)Enumerations.TransactionType.Debit ? Enumerations.TransactionType.Debit.ToString() : Enumerations.TransactionType.Credit.ToString();
        [NotMapped]
        public string PaymentMode => Convert.ToString((PaymentMode)PaymentModeId);
        [NotMapped]
        public string Debit => TransactionType == (int)Enumerations.TransactionType.Debit ? Amount.ToString() : string.Empty;
        [NotMapped]
        public string Credit => TransactionType == (int)Enumerations.TransactionType.Credit ? Amount.ToString() : string.Empty;
        [NotMapped]
        public decimal BalanceLeft
        {
            get
            {
                var totalTransactions = Repository.Instance.All<AmountTransaction>()
                                                           .Where(x => x.MemberAccountId == MemberAccountId
                                                                 && x.StatusId == (int)AmountTransactionStatus.Approved).ToList();
                if (!totalTransactions.Any())
                    return default(decimal);
                return totalTransactions?.Where(y => y.TransactionType == (int)Enumerations.TransactionType.Credit)?.Sum(y => y.Amount) ?? default(decimal) -
                            totalTransactions?.Where(y => y.TransactionType == (int)Enumerations.TransactionType.Debit)?.Sum(y => y.Amount) ?? default(decimal);
            }
        }
        
        [NotMapped]
        public string StatusText => Convert.ToString((AmountTransactionStatus)StatusId);

    }
}