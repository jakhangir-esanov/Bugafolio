using FinancialPortfolioManagement.Service.DTOs.Expenses;

namespace FinancialPortfolioManagement.Service.Interfaces;

public interface IExpenseService
{
    Task<ExpenseResultDto> AddAsync(ExpenseCreationDto dto);
    Task<ExpenseResultDto> ModifyAsync(ExpenseUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<ExpenseResultDto> ReceiveByIdAsync(long id);
    Task<IEnumerable<ExpenseResultDto>> ReceiveAllAsync();
}
