using FinancialPortfolioManagement.Domain.Commons;
using FinancialPortfolioManagement.Domain.Entities.Companies;
using FinancialPortfolioManagement.Domain.Enums;

namespace FinancialPortfolioManagement.Domain.Entities.Employees;

public sealed class Employee : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public Role EmployeeRole { get; set; }
}
