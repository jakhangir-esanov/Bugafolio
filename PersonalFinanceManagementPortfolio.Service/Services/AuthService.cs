using FinancialPortfolioManagement.DAL.IRepositories;
using FinancialPortfolioManagement.Domain.Entities.Employees;
using FinancialPortfolioManagement.Service.Exceptions;
using FinancialPortfolioManagement.Service.Interfaces;
using FinancialPortfolioManagement.Service.Securities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinancialPortfolioManagement.Service.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<Employee> repository;
    private readonly IConfiguration configuration;
    public AuthService(IRepository<Employee> repository, IConfiguration configuration)
    {
        this.repository = repository;
        this.configuration = configuration;
    }
    public async Task<string> GenerateTokenAsync(string email, string password)
    {
        var employee = await repository.GetAsync(x => x.Email.Equals(email))
           ?? throw new NotFoundException("Not found!");

        bool varifiedPassword = PasswordHasher.Verify(password, employee.Password, employee.Salt);
        if (!varifiedPassword)
            throw new CustomException(400,"Inccorect email or password");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
          {
             new Claim("Email", employee.Email),
             new Claim("Id", employee.Id.ToString()),
             new Claim(ClaimTypes.Role, employee.EmployeeRole.ToString())
          }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        string result = tokenHandler.WriteToken(token);
        return result;
    }
}
