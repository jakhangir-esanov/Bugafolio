namespace FinancialPortfolioManagement.Service.DTOs.Budgets;

public class BudgetUpdateDto
{
    public long Id { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public long companyId { get; set; }
}

