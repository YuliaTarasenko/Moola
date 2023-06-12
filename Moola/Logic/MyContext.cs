namespace Moola.Logic;
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
        builder.Entity<User>().HasOne(u => u.Authorization)
            .WithOne(a => a.User).HasForeignKey<Authorization>(a => a.UserId);
        builder.Entity<Category>().HasMany(i => i.Incomes).WithOne(c => c.Category).
            HasForeignKey(c => c.CategoryId).IsRequired();
        builder.Entity<Category>().HasMany(e => e.Expenses).WithOne(c => c.Category).
            HasForeignKey(c => c.CategoryId).IsRequired();
        builder.Entity<Account>().HasMany(i => i.Incomes).WithOne(c => c.Account).
            HasForeignKey(c => c.AccountId).IsRequired();
        builder.Entity<Account>().HasMany(e => e.Expenses).WithOne(c => c.Account).
            HasForeignKey(c => c.AccountId).IsRequired();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
   optionsBuilder.UseSqlServer(
       "Data Source=LAPTOP-47TRIN9C\\MSSQLSERVER01;" +
       "Initial Catalog=Moola;" +
       "Integrated Security=True;" +
       "TrustServerCertificate=True");
}
