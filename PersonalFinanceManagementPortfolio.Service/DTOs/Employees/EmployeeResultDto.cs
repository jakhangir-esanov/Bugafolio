using FinancialPortfolioManagement.Domain.Entities.Companies;
using FinancialPortfolioManagement.Domain.Enums;

namespace FinancialPortfolioManagement.Service.DTOs.Employees;

public class EmployeeResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public Company Company { get; set; }
    public Role EmployeeRole { get; set; }
}
