using FinancialPortfolioManagement.Domain.Entities.Budgets;

namespace FinancialPortfolioManagement.Service.DTOs.Expenses;

public class ExpenseResultDto
{
    public long Id { get; set; }
    public decimal EnterAmount { get; set; } = 0;
    public decimal ExitAmount { get; set; } = 0;
    public DateTime Date { get; set; }

    public Budget Budget { get; set; }
}