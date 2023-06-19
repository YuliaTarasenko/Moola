namespace Moola.Logic
{
    public sealed record IncomeDto(int Id, decimal Amount, DateTime IncomeDate, int FinanceId, int CategoryId, string Note);
}
