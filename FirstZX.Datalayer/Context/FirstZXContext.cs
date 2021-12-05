using FirstZX.Datalayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace FirstZX.Datalayer.Context
{
    public class FirstZXContext:DbContext
    {
        public FirstZXContext(DbContextOptions<FirstZXContext>options):base(options)
        {
            
        }
       

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
       
        public DbSet<BankRequest> BankRequests { get; set; }
        public DbSet<BankDetailEarn> BankDetailEarn { get; set; }
        public DbSet<BankDetailEarnWeek> BankDetailEarnWeek { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserAnswer> UserAnswer { get; set; }
        public DbSet<BankMonth> BankMonths { get; set; }
        public DbSet<BankWeek> BankWeeks { get; set; }


        #endregion

     

    }
}