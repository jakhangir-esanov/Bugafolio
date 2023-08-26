using FinancialPortfolioManagement.Service.DTOs.Employees;

namespace FinancialPortfolioManagement.Service.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResultDto> AddAsync(EmployeeCreationDto dto);
    Task<EmployeeResultDto> ModifyAsync(EmployeeUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<EmployeeResultDto> ReceiveByIdAsync(long id);
    Task<IEnumerable<EmployeeResultDto>> ReceiveAllAsync();
}
