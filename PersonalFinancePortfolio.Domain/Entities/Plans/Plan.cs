using FinancialPortfolioManagement.Domain.Commons;
using FinancialPortfolioManagement.Domain.Entities.Companies;

namespace FinancialPortfolioManagement.Domain.Entities.Plans;

public sealed class Plan : Auditable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public long CompanyId { get; set; }
    public Company Company { get; set; }
}
