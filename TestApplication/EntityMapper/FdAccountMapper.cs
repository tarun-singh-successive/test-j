using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class FdAccountMapper : EntityTypeConfiguration<FdAccount>
    {
        public FdAccountMapper()
        {
            ToTable("FdAccounts");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.MemberAccountId).HasColumnName("MemberAccountId");
            Property(x => x.FdAmount).HasColumnName("FdAmount");
            Property(x => x.IsLessMinAmount).HasColumnName("IsLessMinAmount");
            Property(x => x.DeductAmountFromSavingAccount).HasColumnName("DeductAmountFromSavingAccount");
            Property(x => x.TdsDeduction).HasColumnName("TdsDeduction");
            Property(x => x.JointAccountMemberId).HasColumnName("JointAccountMemberId");
            Property(x => x.IsNominee).HasColumnName("IsNominee");
            Property(x => x.RenewAfterMaturity).HasColumnName("RenewAfterMaturity");
            Property(x => x.PayFromSavingAccount).HasColumnName("PayFromSavingAccount");
            Property(x => x.PenaltyCharges).HasColumnName("PenaltyCharges");
            Property(x => x.SchemeId).HasColumnName("SchemeId");
            Property(x => x.OpenDate).HasColumnName("OpenDate");
            Property(x => x.ClosingDate).HasColumnName("ClosingDate");
            Property(x => x.CreationDate).HasColumnName("CreationDate");
            HasRequired(x => x.FdMemberAccount).WithMany(x => x.FdAccounts).HasForeignKey(x => x.MemberAccountId);
            HasRequired(x => x.Scheme).WithMany(x => x.FdAccounts).HasForeignKey(x => x.SchemeId);
        }
    }
}