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
    }
}
