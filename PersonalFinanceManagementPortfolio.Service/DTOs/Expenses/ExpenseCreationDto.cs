namespace FinancialPortfolioManagement.Service.DTOs.Expenses;

public class ExpenseCreationDto
{
    public decimal EnterAmount { get; set; } = 0;
    public decimal ExitAmount { get; set; } = 0;

    public long budgetId { get; set; }
}
