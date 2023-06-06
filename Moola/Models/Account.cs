namespace Moola.Models
{
    public sealed record Account:EntityWithId
    {
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
        public List<User> Users { get; set; }
        public Account() { }
        public Account(int id, string accountName, decimal amount)
        {
            Id= id; 
            AccountName = accountName; 
            Amount = amount; 
        }
    }
}
