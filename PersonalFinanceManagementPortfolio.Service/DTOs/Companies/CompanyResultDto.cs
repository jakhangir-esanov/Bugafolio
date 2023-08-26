using FinancialPortfolioManagement.Domain.Entities.Employees;

namespace FinancialPortfolioManagement.Service.DTOs.Companies;

public class CompanyResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Industry { get; set; }

    public ICollection<Employee> Employees { get; set; }
}