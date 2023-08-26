using FinancialPortfolioManagement.Domain.Commons;
using FinancialPortfolioManagement.Domain.Entities.Employees;

namespace FinancialPortfolioManagement.Domain.Entities.Companies;

public sealed class Company : Auditable
{
    public string Name { get; set; }
    public string Industry { get; set; }

    public ICollection<Employee> Employees { get; set; }
}
