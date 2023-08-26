using FinancialPortfolioManagement.Service.DTOs.Budgets;
using FinancialPortfolioManagement.Service.DTOs.Expenses;

namespace FinancialPortfolioManagement.Service.Interfaces;

public interface IBudgetService
{
    Task<BudgetResultDto> AddAsync(BudgetCreationDto dto);
    Task<BudgetResultDto> ModifyAsync(BudgetUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<BudgetResultDto> ReceiveByIdAsync(long id);
    Task<IEnumerable<BudgetResultDto>> ReceiveAllAsync();
    Task<IEnumerable<ExpenseResultDto>> ReceiveAllExpensesAsync(long budgetId);
}
