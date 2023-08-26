namespace FinancialPortfolioManagement.Service.DTOs.Expenses;

public class ExpenseUpdateDto
{
    public long Id { get; set; }
    public decimal EnterAmount { get; set; } = 0;
    public decimal ExitAmount { get; set; } = 0;

    public long budgetId { get; set; }
}
