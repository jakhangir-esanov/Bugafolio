using FinancialPortfolioManagement.Domain.Commons;
using FinancialPortfolioManagement.Domain.Entities.Budgets;

namespace FinancialPortfolioManagement.Domain.Entities.Expenses;

public sealed class Expense : Auditable
{
    public decimal EnterAmount { get; set; } = 0;
    public decimal ExitAmount { get; set; } = 0;
    public DateTime Date { get; set; } = DateTime.Now;

    public long budgetId { get; set; }
    public Budget Budget { get; set; }
}
