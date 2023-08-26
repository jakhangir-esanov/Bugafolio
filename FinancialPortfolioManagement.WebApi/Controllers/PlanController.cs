using FinancialPortfolioManagement.Service.DTOs.Plans;
using FinancialPortfolioManagement.Service.Interfaces;
using FinancialPortfolioManagement.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlanController : ControllerBase
{
    private readonly IPlanService planService;
    public PlanController(IPlanService planService)
    {
        this.planService = planService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddAsync(PlanCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await planService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(PlanUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await planService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await planService.RemoveAsync(id)
        });

    [Authorize(Roles = "Manager")]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetById(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await planService.ReceiveByIdAsync(id)
        });

    [Authorize(Roles = "Manager")]
    [HttpGet("get-for-pagination")]
    public async Task<IEnumerable<PlanResultDto>> GetForPagination(int page = 1, int pageSize = 10)
    {
        var totalInfo = await planService.ReceiveAllAsync();
        var totalCount = totalInfo.Count();
        var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        var perPage = totalInfo
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return perPage;
    }
}
