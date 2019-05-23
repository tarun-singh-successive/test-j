using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class MemberAccountNomineeMapper : EntityTypeConfiguration<MemberAccountNominee>
    {
        public MemberAccountNomineeMapper()
        {
            ToTable("MemberAccountNominee");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Address).HasColumnName("Address");
            Property(x => x.AssignedDate).HasColumnName("AssignedDate");
            Property(x => x.Dob).HasColumnName("Dob");
            //Property(x => x.MemberId).HasColumnName("MemberId");
            Property(x => x.Mobile).HasColumnName("Mobile");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.Relationship).HasColumnName("Relationship");
            HasRequired(x => x.Member).WithMany(y=>y.MemberAccountNominees).HasForeignKey(x => x.MemberId);
        }
    }
}