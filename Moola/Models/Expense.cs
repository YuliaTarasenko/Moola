namespace Moola.Models
{
    public sealed record Expense:EntityWithId
    {
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string Note { get; set; }
        public int AccountId { get; set; }
        public Category Category { get; set; }
        public Account Account { get; set; }

        public Expense() { }
        public Expense(int id, decimal amount, int categoryId, string note, int accountId)
        {
            Id = id;
            Amount = amount;
            CategoryId = categoryId;
            Note = note;
            AccountId = accountId;
        }
    }
}
