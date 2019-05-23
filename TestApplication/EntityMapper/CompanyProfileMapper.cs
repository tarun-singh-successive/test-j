using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class CompanyProfileMapper : EntityTypeConfiguration<CompanyProfile>
    {
        public CompanyProfileMapper()
        {
            ToTable("CompanyProfile");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Email).HasColumnName("Email");
            Property(x => x.AboutCompany).HasColumnName("AboutCompany");
            Property(x => x.Address1).HasColumnName("Address1");
            Property(x => x.Address2).HasColumnName("Address2");
            Property(x => x.AuthorizedCapital).HasColumnName("AuthorizedCapital");
            Property(x => x.CIN).HasColumnName("CIN");
            Property(x => x.City).HasColumnName("City");
            Property(x => x.CompanyClass).HasColumnName("CompanyClass");
            Property(x => x.CompanyName).HasColumnName("CompanyName");
            Property(x => x.CountryId).HasColumnName("CountryId");
            Property(x => x.IncorporationCountryId).HasColumnName("IncorporationCountryId");
            Property(x => x.IncorporationDate).HasColumnName("IncorporationDate");
            Property(x => x.IncorporationStateId).HasColumnName("IncorporationStateId");
            Property(x => x.Landline).HasColumnName("Landline");
            Property(x => x.Mobile).HasColumnName("Mobile");
            Property(x => x.PaidupCapital).HasColumnName("PaidupCapital");
            Property(x => x.PAN).HasColumnName("PAN");
            Property(x => x.Pincode).HasColumnName("Pincode");
            Property(x => x.ShareNominalValue).HasColumnName("ShareNominalValue");
            Property(x => x.ShortName).HasColumnName("ShortName");
            Property(x => x.StateId).HasColumnName("StateId");
            Property(x => x.StateOfRegistration).HasColumnName("StateOfRegistration");
            Property(x => x.TAN).HasColumnName("TAN");
            Property(x => x.LastModificationDate).HasColumnName("LastModificationDate");
            HasRequired(x => x.Country).WithMany().HasForeignKey(x => x.CountryId);
            HasRequired(x => x.Country).WithMany().HasForeignKey(x => x.IncorporationCountryId);
            HasRequired(x => x.State).WithMany().HasForeignKey(x => x.StateId);
            HasRequired(x => x.State).WithMany().HasForeignKey(x => x.IncorporationStateId);
        }
    }
}