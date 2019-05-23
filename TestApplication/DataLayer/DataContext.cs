using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TestApplication.Entities;
using TestApplication.EntityMapper;

namespace TestApplication.DataLayer
{
    public class DataContext: DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserInfoMapper());
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Configurations.Add(new CompanyProfileMapper());
            modelBuilder.Configurations.Add(new StateMapper());
            modelBuilder.Configurations.Add(new CountryMapper());
            modelBuilder.Configurations.Add(new MemberMapper());
            modelBuilder.Configurations.Add(new MemberAccountNomineeMapper());
            modelBuilder.Configurations.Add(new ShareHoldingMapper());
            modelBuilder.Configurations.Add(new GroupsMapper());
            modelBuilder.Entity<MembersInGroup>().HasKey(x => new { x.MemberId, x.GroupId });
            modelBuilder.Configurations.Add(new BranchesMapper());
            modelBuilder.Configurations.Add(new InmateTbScreenMapper());
            modelBuilder.Configurations.Add(new SchemeMapper());
            modelBuilder.Configurations.Add(new AmountTransactionsMapper());
            modelBuilder.Configurations.Add(new MemberAccountsMapper());
            modelBuilder.Configurations.Add(new TransactionPurposeMapper());
            modelBuilder.Configurations.Add(new CompanyBankAccountMapper());
            modelBuilder.Configurations.Add(new FdAccountMapper());
        }
    }
}