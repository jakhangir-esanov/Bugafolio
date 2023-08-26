using FinancialPortfolioManagement.Domain.Commons;
using FinancialPortfolioManagement.Domain.Entities.Companies;
using FinancialPortfolioManagement.Domain.Entities.Expenses;

namespace FinancialPortfolioManagement.Domain.Entities.Budgets;

public sealed class Budget : Auditable
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetIncome { get; set; }

    public long companyId { get; set; }
    public Company Company { get; set; }
    public ICollection<Expense> Expenses { get; set; }
}
