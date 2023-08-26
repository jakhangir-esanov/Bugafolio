using FinancialPortfolioManagement.Domain.Entities.Companies;

namespace FinancialPortfolioManagement.Service.DTOs.Plans;

public class PlanResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public Company Company { get; set; }
}