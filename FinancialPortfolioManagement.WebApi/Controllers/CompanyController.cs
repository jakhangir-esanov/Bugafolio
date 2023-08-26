using FinancialPortfolioManagement.Service.DTOs.Companies;
using FinancialPortfolioManagement.Service.Interfaces;
using FinancialPortfolioManagement.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService companyService;
    public CompanyController(ICompanyService companyService)
    {
        this.companyService = companyService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddAsync(CompanyCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await companyService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(CompanyUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await companyService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await companyService.RemoveAsync(id)
        });

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await companyService.ReceiveByIdAsync(id)
        });

    [Authorize(Roles = "Manager")]
    [HttpGet("get-employee/{id:long}")]
    public async Task<IActionResult> GetEmployeesAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await companyService.ReceiveAllEmployeesAsync(id)
        });

    [Authorize(Roles = "Manager")]
    [HttpGet("get-for-pagination")]
    public async Task<IEnumerable<CompanyResultDto>> GetForPagination(int page = 1, int pageSize = 10)
    {
        var totalInfo = await companyService.ReceiveAllAsync();
        var totalCount = totalInfo.Count();
        var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        var perPage = totalInfo
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return perPage;
    }
}
