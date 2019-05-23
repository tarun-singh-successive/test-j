using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class MembersInGroupMapper : EntityTypeConfiguration<MembersInGroup>
    {
        public MembersInGroupMapper()
        {
            ToTable("MembersInGroup");
            HasKey(x => new { x.MemberId, x.GroupId });
            HasRequired(x => x.Member).WithMany(y => y.MembersInGroup).HasForeignKey(x => x.MemberId);
            HasRequired(x => x.Group).WithMany(y => y.MembersInGroup).HasForeignKey(x => x.GroupId);
        }
    }
}