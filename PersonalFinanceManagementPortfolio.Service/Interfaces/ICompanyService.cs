using FinancialPortfolioManagement.Service.DTOs.Companies;
using FinancialPortfolioManagement.Service.DTOs.Employees;

namespace FinancialPortfolioManagement.Service.Interfaces;

public interface ICompanyService
{
    Task<CompanyResultDto> AddAsync(CompanyCreationDto dto);
    Task<CompanyResultDto> ModifyAsync(CompanyUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<CompanyResultDto> ReceiveByIdAsync(long id);
    Task<IEnumerable<CompanyResultDto>> ReceiveAllAsync();
    Task<IEnumerable<EmployeeResultDto>> ReceiveAllEmployeesAsync(long companyId);
}
