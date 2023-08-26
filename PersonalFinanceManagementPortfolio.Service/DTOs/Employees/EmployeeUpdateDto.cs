using FinancialPortfolioManagement.Domain.Enums;

namespace FinancialPortfolioManagement.Service.DTOs.Employees;

public class EmployeeUpdateDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public long CompanyId { get; set; }
    public Role EmployeeRole { get; set; }
}
