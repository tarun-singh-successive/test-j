using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class UserInfoMapper : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoMapper()
        {
            ToTable("UserInfo");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserEmail).HasColumnName("UserEmail");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.PhoneNumber).HasColumnName("PhoneNumber");
            //HasRequired(x => x.ApplicationUser).WithRequired().Map(x => x.MapKey("UserId"));
            HasRequired(x => x.ApplicationUser).WithMany().HasForeignKey(x => x.UserId);
            //HasMany(x => x.ApplicationUser).WithRequired().HasForeignKey(x => x.ArticleId)
        }
    }
}