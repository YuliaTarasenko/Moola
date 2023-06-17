namespace Moola.Models
{
    public sealed record Expense : EntityWithId
    {
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string? Note { get; set; }
        public int FinanceId { get; set; }
        public Category Category { get; set; }
        public Finance Finance { get; set; }

        public Expense()
        {
        }

        public Expense(int id, DateTime expenseDate, decimal amount, int categoryId, string? note, int financeId)
        {
            Id = id;
            ExpenseDate = expenseDate;
            Amount = amount;
            CategoryId = categoryId;
            Note = note;
            FinanceId = financeId;
        }
    }
}
