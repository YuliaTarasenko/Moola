namespace Moola.Models
{
    public sealed record Category : EntityWithId
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
    }
}
