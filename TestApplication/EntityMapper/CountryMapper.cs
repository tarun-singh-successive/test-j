using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class CountryMapper : EntityTypeConfiguration<Country>
    {
        public CountryMapper()
        {
            ToTable("Countries");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.SortName).HasColumnName("SortName");
            Property(x => x.PhoneCode).HasColumnName("PhoneCode");
        }
    }
}