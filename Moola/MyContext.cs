using Moola.Models;
using Microsoft.EntityFrameworkCore;

namespace Moola
{
    public sealed class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Authorization>().HasMany(a => a.Users).WithOne(u => u.Authorization).
                HasForeignKey(u => u.AuthorizationId).IsRequired();
            builder.Entity<Category>().HasMany(i=>i.Incomes).WithOne(c=>c.Category).
                HasForeignKey(c=> c.CategoryId).IsRequired();
            builder.Entity<Category>().HasMany(e => e.Expenses).WithOne(c => c.Category).
                HasForeignKey(c => c.CategoryId).IsRequired();
            builder.Entity<Account>().HasMany(i => i.Incomes).WithOne(c => c.Account).
                HasForeignKey(c => c.AccountId).IsRequired();
            builder.Entity<Account>().HasMany(e => e.Expenses).WithOne(c => c.Account).
                HasForeignKey(c => c.AccountId).IsRequired();
            builder.Entity<Account>().HasMany(u=>u.Users).WithMany(a=>a.Accounts).
                UsingEntity<UserAccount>(
                u=>u.HasOne(ua=>ua.User).WithMany().HasForeignKey(ua=>ua.UserId),
                a=>a.HasOne(ua=>ua.Account).WithMany().HasForeignKey(ua=>ua.AccountId),
                ua => ua.HasKey(c => new {c.UserId, c.AccountId}));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
       optionsBuilder.UseSqlServer(
           "Data Source=LAPTOP-47TRIN9C\\MSSQLSERVER01;" + "Initial Catalog=Moola;"
           + "Integrated Security=True;" + "TrustServerCertificate = True");
    }
}
