using FinancialPortfolioManagement.Domain.Entities.Companies;
using FinancialPortfolioManagement.Domain.Entities.Expenses;

namespace FinancialPortfolioManagement.Service.DTOs.Budgets;

public class BudgetResultDto
{
    public long Id { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetIncome { get; set; }

    public Company Company { get; set; }
    public ICollection<Expense> Expenses { get; set; }
}
