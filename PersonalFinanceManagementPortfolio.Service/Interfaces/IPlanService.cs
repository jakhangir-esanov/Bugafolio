using FinancialPortfolioManagement.Service.DTOs.Plans;

namespace FinancialPortfolioManagement.Service.Interfaces;

public interface IPlanService
{
    Task<PlanResultDto> AddAsync(PlanCreationDto dto);
    Task<PlanResultDto> ModifyAsync(PlanUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<PlanResultDto> ReceiveByIdAsync(long id);
    Task<IEnumerable<PlanResultDto>> ReceiveAllAsync();
}
