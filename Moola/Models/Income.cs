namespace Moola.Models
{
    public sealed record Income : EntityWithId
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime IncomeDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        [StringLength(100)]
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
