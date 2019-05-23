using System.Data.Entity.ModelConfiguration;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class StateMapper : EntityTypeConfiguration<State>
    {
        public StateMapper()
        {
            ToTable("States");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.CountryId).HasColumnName("CountryId");
            HasRequired(x => x.Country).WithMany().HasForeignKey(x => x.CountryId);
        }
    }
}