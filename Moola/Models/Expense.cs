namespace Moola.Models
{
    public sealed record Expense : EntityWithId
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }
        [Required]
        [Range(0,999999)]
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        [StringLength(100)]
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
