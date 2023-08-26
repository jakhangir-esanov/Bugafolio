using FinancialPortfolioManagement.Service.DTOs.Employees;
using FinancialPortfolioManagement.Service.Interfaces;
using FinancialPortfolioManagement.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService employeeService;
    public EmployeeController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddAsync(EmployeeCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await employeeService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(EmployeeUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await employeeService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await employeeService.RemoveAsync(id)
        });

    [Authorize(Roles = "Manager")]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await employeeService.ReceiveByIdAsync(id)
        });

    [Authorize(Roles = "Manager")]
    [HttpGet("get-for-pagination")]
    public async Task<IEnumerable<EmployeeResultDto>> GetForPagination(int page = 1, int pageSize = 10)
    {
        var totalInfo = await employeeService.ReceiveAllAsync();
        var totalCount = totalInfo.Count();
        var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        var perPage = totalInfo
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return perPage;
    }
}
