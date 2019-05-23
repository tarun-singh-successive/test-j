using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class MemberMapper : EntityTypeConfiguration<Member>
    {
        public MemberMapper()
        {
            ToTable("Member");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); ;
            Property(x => x.AgentId).HasColumnName("AgentId");
            Property(x => x.GroupId).HasColumnName("GroupId");
            Property(x => x.BranchId).HasColumnName("BranchId");
            Property(x => x.EnrollmentDate).HasColumnName("EnrollmentDate");
            Property(x => x.Title).HasColumnName("Title");
            Property(x => x.Gender).HasColumnName("Gender");
            Property(x => x.FirstName).HasColumnName("FirstName");
            Property(x => x.MiddleName).HasColumnName("MiddleName");
            Property(x => x.LastName).HasColumnName("LastName");
            Property(x => x.DOB).HasColumnName("DOB");
            Property(x => x.Occupation).HasColumnName("Occupation");
            Property(x => x.Email).HasColumnName("Email");
            Property(x => x.Mobile).HasColumnName("Mobile");
            Property(x => x.Address1).HasColumnName("Address1");
            Property(x => x.Address2).HasColumnName("Address2");
            Property(x => x.City).HasColumnName("City");
            Property(x => x.StateId).HasColumnName("StateId");
            Property(x => x.CountryId).HasColumnName("CountryId");
            Property(x => x.AadharNumber).HasColumnName("AadharNumber");
            Property(x => x.VoterId).HasColumnName("VoterId");
            Property(x => x.PanNo).HasColumnName("PanNo");
            Property(x => x.FatherName).HasColumnName("FatherName");
            Property(x => x.HusbandWifeName).HasColumnName("HusbandWifeName");
            Property(x => x.IsMinor).HasColumnName("IsMinor");
            Property(x => x.MemberAccountId).HasColumnName("MemberAccountId");
            
            HasRequired(x => x.Country).WithMany().HasForeignKey(x => x.CountryId);
            HasOptional(x => x.MemberAccount).WithMany().HasForeignKey(x => x.MemberAccountId);
            HasRequired(x => x.State).WithMany().HasForeignKey(x => x.StateId);
            //HasRequired(x => x.Nominee).WithMany().HasForeignKey(x => x.NomineeId);
            Property(x => x.IsPromoter).HasColumnName("IsPromoter");
        }
    }
}