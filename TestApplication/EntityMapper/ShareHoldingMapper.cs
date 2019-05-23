using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TestApplication.Entities;

namespace TestApplication.EntityMapper
{
    public class ShareHoldingMapper: EntityTypeConfiguration<ShareHolding>
    {
        public ShareHoldingMapper()
        {
            ToTable("ShareHoldings");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreationDate).HasColumnName("CreationDate");
            Property(x => x.MemberId).HasColumnName("MemberId");
            Property(x => x.PricePerShare).HasColumnName("PricePerShare");
            Property(x => x.Quantity).HasColumnName("Quantity");
            Property(x => x.ReferId).HasColumnName("ReferId");
            HasRequired(x => x.Member).WithMany().HasForeignKey(x => x.MemberId);
            Property(x => x.TransferDate).HasColumnName("TransferDate");

        }
    }
}