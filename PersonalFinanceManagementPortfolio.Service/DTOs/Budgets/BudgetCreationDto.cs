namespace FinancialPortfolioManagement.Service.DTOs.Budgets;

public class BudgetCreationDto
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public long companyId { get; set; }
}

