using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class BranchesMapper : EntityTypeConfiguration<Branches>
    {
        public BranchesMapper()
        {
            ToTable("Branches");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.OpeningDate).HasColumnName("OpeningDate");
            Property(x => x.City).HasColumnName("City");
            Property(x => x.StateId).HasColumnName("StateId");
            HasRequired(x => x.State).WithMany().HasForeignKey(x => x.StateId);
        }
    }
}