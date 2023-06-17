namespace Moola.Models
{
    public sealed record Income : EntityWithId
    {
        public DateTime IncomeDate { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string? Note { get; set; }
        public int FinanceId { get; set; }
        public Category Category { get; set; }
        public Finance Finance { get; set; }

        public Income()
        {
        }

        public Income(int id, DateTime incomeDate, decimal amount, int categoryId, string? note, int financeId)
        {
            Id = id;
            IncomeDate = incomeDate;
            Amount = amount;
            CategoryId = categoryId;
            Note = note;
            FinanceId = financeId;
        }
    }
}
