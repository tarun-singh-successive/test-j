using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApplication.Helpers
{
    public class Enumerations
    {
        public struct UserRoles
        {
            public const string Admin = "Admin";

            public const string SubAdmin = "SubAdmin";

            public const string User = "User";
        }

        public enum CompanyCategory
        {
            [Description("Limited by Shares")]
            LimitedByShares = 0,

            [Description("Limited by Guarantee")]
            LimitedByGuarantee = 1,

            [Description("Unlimited Company")]
            UnlimitedCompany = 2
        }

        public enum TenureType
        {
            Days = 1,
            Months = 2,
            Years = 3,
        }

        public enum InterestPayoutType
        {
            [Description("Monthly")]
            Monthly = 1,

            [Description("Quarterly")]
            Quarterly = 2,

            [Description("Half Yearly")]
            HalfYearly = 3,

            [Description("Yearly")]
            Yearly = 4,

            [Description("Cumulative Monthly")]
            CumulativeMonthly = 5,

            [Description("Cumulative Quarterly")]
            CumulativeQuarterly = 6,

            [Description("Cumulative Half Yearly")]
            CumulativeHalfYearly = 7,

            [Description("Cumulative Yearly")]
            CumulativeYearly = 8,
        }

        public enum AccountType
        {
            Fd = 1,
            SavingAccount = 2,
            Rd = 2,
            GoldLoan = 3
        }

        public enum TransactionType
        {
            Credit = 1,
            Debit = 2
        }

        public enum MemberAccountStatus
        {
            [Description("Pending for Approval")]
            PendingForApproval = 0,

            [Description("In Active")]
            InActive = 1,

            [Description("Active")]
            Active = 2,

            [Description("Fore Close Requested")]
            ForeCloseRequested = 3,

            [Description("Fore Close Approved")]
            ForeCloseApproved = 4,

            [Description("Fore Closed")]
            ForeClosed = 5,

            [Description("Completed")]
            Completed = 4,
        }

        public enum TransactionPurposeType
        {
            [Description("Deposit")]
            Deposit,
            Withdraw,
            OtherCharges,
            Closing,
        }

        public enum PaymentMode
        {
            Cash = 1,
            Cheque = 2,
            OnlineTransaction = 3,
        }

        public enum AmountTransactionStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }

        public enum AccountDashboardPages
        {
            Deposit = 3,
            Withdraw = 4,
            OtherCharges = 5,
            CloseAccount = 6,
        }

        public enum OnlineTransferModes
        {
            [Description("IMPS")]
            Imps = 1,
            [Description("VPA")]
            Vpa = 2,
            [Description("NEFT/RTGS")]
            Neft = 3,
        }

        public enum PayementModeId
        {
            [Description("Cash")]
            Cash = 1,
            [Description("Cheque")]
            Cheque = 2,
            [Description("Online Tr.")]
            OnlineTransaction = 3
        }

    }
}