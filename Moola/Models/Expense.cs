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
    }
}
