using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class InmateTbScreenMapper : EntityTypeConfiguration<InmateTbScreens>
    {
        public InmateTbScreenMapper()
        {
            ToTable("InmateTbScreens");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Comment).HasColumnName("Comment");
            Property(x => x.InmateId).HasColumnName("InmateId");
            Property(x => x.CreationDate).HasColumnName("CreationDate");
            Property(x => x.PpdApplied).HasColumnName("PpdApplied");
        }

    }
}