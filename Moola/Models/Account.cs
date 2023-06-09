﻿namespace Moola.Models
{
    public sealed record Account
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
        public List<User> Users { get; set; }
    }
}
