using FinancialPortfolioManagement.Service.DTOs.Expenses;
using FinancialPortfolioManagement.Service.Interfaces;
using FinancialPortfolioManagement.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService expenseService;
    public ExpenseController(IExpenseService expenseService)
    {
        this.expenseService = expenseService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddAsync(ExpenseCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await expenseService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(ExpenseUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await expenseService.ModifyAsync(dto)
        });

    [HttpDelete("delete{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await expenseService.RemoveAsync(id)
        });

    [Authorize(Roles = "Manager")]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await expenseService.ReceiveByIdAsync(id)
        });

    [Authorize(Roles = "Manager")]
    [HttpGet("get-for-pagination")]
    public async Task<IEnumerable<ExpenseResultDto>> GetForPagination(int page = 1, int pageSize = 10)
    {
        var totalInfo = await expenseService.ReceiveAllAsync();
        var totalCount = totalInfo.Count();
        var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        var perPage = totalInfo
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return perPage;
    }
}

