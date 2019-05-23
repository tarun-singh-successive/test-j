using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class TransactionPurposeMapper : EntityTypeConfiguration<TransactionPurpose>
    {
        public TransactionPurposeMapper()
        {
            ToTable("TransactionPurpose");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.Charges).HasColumnName("Charges");
            Property(x => x.IsActive).HasColumnName("IsActive");
        }
    }
}