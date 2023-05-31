using System.Linq.Expressions;

namespace Moola.Models
{
    public sealed record Category:EntityWithId
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
        public Category() { }
        public Category(int id, string name, string color)
        {
            Id = id;
            Name = name;
            Color = color;
        }
    }
}
