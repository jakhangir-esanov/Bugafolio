namespace FinancialPortfolioManagement.Service.DTOs.Plans;

public class PlanUpdateDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public long CompanyId { get; set; }
}
