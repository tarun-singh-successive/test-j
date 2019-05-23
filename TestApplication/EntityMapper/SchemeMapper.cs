using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class SchemeMapper : EntityTypeConfiguration<Scheme>
    {
        public SchemeMapper()
        {
            ToTable("Schemes");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.AnnualInterestRate).HasColumnName("AnnualInterestRate");
            Property(x => x.CancellationCharges).HasColumnName("CancellationCharges");
            Property(x => x.CreationDate).HasColumnName("CreationDate");
            Property(x => x.InterestLockinPeriod).HasColumnName("InterestLockinPeriod");
            Property(x => x.InterestPayType).HasColumnName("InterestPayType");
            Property(x => x.IsActive).HasColumnName("IsActive");
            Property(x => x.LastModificationDate).HasColumnName("LastModificationDate");
            Property(x => x.MinimumAmount).HasColumnName("MinimumAmount");
            Property(x => x.MinimumMonthlyAverageBalance).HasColumnName("MinimumMonthlyAverageBalance");
            Property(x => x.PrincipleLockInPeriod).HasColumnName("PrincipleLockInPeriod");
            Property(x => x.SchemeCode).HasColumnName("SchemeCode");
            Property(x => x.SchemeTypeId).HasColumnName("SchemeTypeId");
            Property(x => x.TenureInterval).HasColumnName("TenureInterval");
            Property(x => x.TenureType).HasColumnName("TenureType");
        }
    }
}