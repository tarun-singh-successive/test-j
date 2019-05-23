using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;
    
namespace TestApplication.EntityMapper
{
    public class GroupsMapper : EntityTypeConfiguration<Group>
    {
        public GroupsMapper()
        {
            ToTable("Groups");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.IsActive).HasColumnName("IsActive");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.CreationDate).HasColumnName("CreationDate");
            //.HasMany(t => t.MemberIds)
            //.WithMany(t => t.Courses)
            //.Map(m =>
            //{
            //    m.ToTable("CourseInstructor");
            //    m.MapLeftKey("CourseID");
            //    m.MapRightKey("InstructorID");
            //});
            //HasMany<Member>(s => s.Members)
            //    .WithMany(c => c.Groups)
            //    .Map(cs =>
            //    {
            //        cs.MapLeftKey("MemberId");
            //        cs.MapRightKey("GroupId");
            //        cs.ToTable("MembersInGroup");
            //    });
        }
    }
}