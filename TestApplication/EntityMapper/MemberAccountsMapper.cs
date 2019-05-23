using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class MemberAccountsMapper : EntityTypeConfiguration<MemberAccount>
    {
        public MemberAccountsMapper()
        {
            ToTable("MemberAccounts");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AgentId).HasColumnName("AgentId");
            Property(x => x.MinAmountCheckbox).HasColumnName("MinAmountCheckbox");
            Property(x => x.MemberId).HasColumnName("MemberId");
            Property(x => x.SchemeId).HasColumnName("SchemeId");
            Property(x => x.CreationDate).HasColumnName("CreationDate");
            Property(x => x.ClosingDate).HasColumnName("ClosingDate");
            Property(x => x.StatusId).HasColumnName("StatusId");
            Property(x => x.JointAccountMemberId).HasColumnName("JointAccountMemberId");
            Property(x => x.AccountTypeId).HasColumnName("AccountTypeId");
            Property(x => x.MinAmount).HasColumnName("MinAmount");
            Property(x => x.AgentCollectionId).HasColumnName("AgentCollectionId");
            HasRequired(x => x.Branches).WithMany().HasForeignKey(x => x.BranchId);
            HasRequired(x => x.Member).WithMany().HasForeignKey(x => x.MemberId);
            HasRequired(x => x.Scheme).WithMany().HasForeignKey(x => x.SchemeId);
            //HasRequired(x => x.Agent).WithMany().HasForeignKey(x => x.AgentId);
        }
    }
}