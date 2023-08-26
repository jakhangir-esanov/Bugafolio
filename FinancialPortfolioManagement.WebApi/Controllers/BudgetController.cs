using FinancialPortfolioManagement.Service.DTOs.Budgets;
using FinancialPortfolioManagement.Service.Interfaces;
using FinancialPortfolioManagement.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService budgetService;
    public BudgetController(IBudgetService budgetService)
    {
        this.budgetService = budgetService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(BudgetCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await budgetService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> PutAsync(BudgetUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await budgetService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await budgetService.RemoveAsync(id)
        });

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await budgetService.ReceiveByIdAsync(id)
        });

    [Authorize(Roles = "Manager")]
    [HttpGet("get-expense/{id:long}")]
    public async Task<IActionResult> GetExpenseAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await budgetService.ReceiveAllExpensesAsync(id)
        });

    [Authorize(Roles = "Manager")]
    [HttpGet("get-for-pagination")]
    public async Task<IEnumerable<BudgetResultDto>> GetForPagination(int page = 1, int pageSize = 10)
    {
        var totalInfo = await budgetService.ReceiveAllAsync();
        var totalCount = totalInfo.Count();
        var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        var perPage = totalInfo
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return perPage;
    }
}
