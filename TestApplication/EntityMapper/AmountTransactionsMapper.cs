using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class AmountTransactionsMapper : EntityTypeConfiguration<AmountTransaction>
    {
        public AmountTransactionsMapper()
        {
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id");
            Property(x => x.MemberAccountId).HasColumnName("MemberAccountId");
            Property(x => x.ChequeBankId).HasColumnName("ChequeBankId");
            Property(x => x.ChequeDate).HasColumnName("ChequeDate");
            Property(x => x.ChequeNo).HasColumnName("ChequeNo");
            Property(x => x.TransactionType).HasColumnName("TransactionType");
            Property(x => x.TransactionDate).HasColumnName("TransactionDate");
            Property(x => x.CreationDate).HasColumnName("CreationDate");
            Property(x => x.Amount).HasColumnName("Amount");
            Property(x => x.PaymentModeId).HasColumnName("PaymentModeId");
            Property(x => x.OnlineTransferModeId).HasColumnName("OnlineTransferModeId");
            Property(x => x.OnlineTransferId).HasColumnName("OnlineTransferId");
            Property(x => x.OnlineTransferDate).HasColumnName("OnlineTransferDate");
            Property(x => x.CreditInCompany).HasColumnName("CreditInCompany");
            Property(x => x.LastModificationDateTime).HasColumnName("LastModificationDate");
            Property(x => x.TransactionPurposeId).HasColumnName("TransactionPurposeId");
            Property(x => x.ApprovedBy).HasColumnName("ApprovedBy");
            Property(x => x.ApprovedDate).HasColumnName("ApprovedDate");
            Property(x => x.ApprovedNote).HasColumnName("ApprovedNote");
            Property(x => x.ChequeClearingDate).HasColumnName("ChequeClearingDate");
            Property(x => x.CreatedBy).HasColumnName("CreatedBy");
            Property(x => x.CompanyBankAccountId).HasColumnName("CompanyBankAccountId");
            Property(x => x.IsDelete).HasColumnName("IsDelete");
            Property(x => x.TransactionPurposeId).HasColumnName("TransactionPurposeId");
            Property(x => x.StatusId).HasColumnName("StatusId");
            Property(x => x.Remarks).HasColumnName("Remarks");

            HasRequired(x => x.MemberAccount).WithMany().HasForeignKey(x => x.MemberAccountId);
            HasRequired(x => x.TransactionPurpose).WithMany().HasForeignKey(x => x.TransactionPurposeId);
        }
    }
}