namespace Moola.Logic;
public sealed class MyContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Finance> Finances { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasOne(u => u.Account)
            .WithOne(a => a.User).HasForeignKey<Account>(a => a.UserId);
        builder.Entity<Category>().HasMany(i => i.Incomes).WithOne(c => c.Category).
            HasForeignKey(c => c.CategoryId).IsRequired();
        builder.Entity<Category>().HasMany(e => e.Expenses).WithOne(c => c.Category).
            HasForeignKey(c => c.CategoryId).IsRequired();
        builder.Entity<Finance>().HasMany(i => i.Incomes).WithOne(c => c.Finance).
            HasForeignKey(c => c.FinanceId).IsRequired();
        builder.Entity<Finance>().HasMany(e => e.Expenses).WithOne(c => c.Finance).
            HasForeignKey(c => c.FinanceId).IsRequired();
    }
}
