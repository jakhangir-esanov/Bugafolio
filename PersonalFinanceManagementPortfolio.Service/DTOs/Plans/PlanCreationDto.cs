namespace FinancialPortfolioManagement.Service.DTOs.Plans;

public class PlanCreationDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public long CompanyId { get; set; }
}
