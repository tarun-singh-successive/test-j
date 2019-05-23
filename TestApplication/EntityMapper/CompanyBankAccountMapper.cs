using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class CompanyBankAccountMapper : EntityTypeConfiguration<CompanyBankAccount>
    {
        public CompanyBankAccountMapper()
        {
            ToTable("CompanyBankAccounts");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.AccountNo).HasColumnName("AccountNo");
            Property(x => x.IfscCode).HasColumnName("IfscCode");
            Property(x => x.OpenDate).HasColumnName("OpenDate");
            Property(x => x.IsActive).HasColumnName("IsActive");
        }
    }
}