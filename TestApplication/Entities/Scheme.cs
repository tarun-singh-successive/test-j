using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestApplication.Helpers;
using static TestApplication.Helpers.Enumerations;

namespace TestApplication.Entities
{
    public class Scheme
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Scheme Name")]
        public string Name { get; set; }

        [Display(Name = "Scheme Code")]
        public string SchemeCode { get; set; }
        public decimal? MinimumAmount { get; set; }
        public int SchemeTypeId { get; set; }
        public int TenureType { get; set; }
        public int TenureInterval { get; set; }
        public int InterestPayType { get; set; }
        public DateTime CreationDate { get; set; }

        [Display(Name = "Annual Interest Rate")]
        public decimal AnnualInterestRate { get; set; }
        public int PrincipleLockInPeriod { get; set; }
        public int InterestLockinPeriod { get; set; }

        [Display(Name = "Cancellation Charges (if any)")]
        public decimal? CancellationCharges { get; set; }
        public DateTime LastModificationDate { get; set; } = DateTime.Now;
        public decimal? MinimumMonthlyAverageBalance { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public string Tenure => $"{TenureInterval} {((TenureType)TenureType).ToString()}";

        public string InterestPayTypeString => ((InterestPayoutType)InterestPayType).GetDescription();
        public string MaturityDate
        {
            get
            {
                switch (TenureType)
                {
                    case (int)Enumerations.TenureType.Days:
                        return CreationDate.AddDays(TenureInterval).ToShortDateString();
                    case (int)Enumerations.TenureType.Months:
                        return CreationDate.AddMonths(TenureInterval).ToShortDateString();
                    case (int)Enumerations.TenureType.Years:
                        return CreationDate.AddYears(TenureInterval).ToShortDateString();
                    default: return string.Empty;
                }
            }
        }

        public ICollection<FdAccount> FdAccounts { get; set; }
    }
}