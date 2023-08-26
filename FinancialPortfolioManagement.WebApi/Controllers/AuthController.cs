using FinancialPortfolioManagement.Service.Interfaces;
using FinancialPortfolioManagement.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPortfolioManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> GenerateTokenAsync(string email, string password)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await authService.GenerateTokenAsync(email, password)
        });
}
