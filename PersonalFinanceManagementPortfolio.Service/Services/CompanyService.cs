using AutoMapper;
using FinancialPortfolioManagement.DAL.IRepositories;
using FinancialPortfolioManagement.Domain.Entities.Companies;
using FinancialPortfolioManagement.Service.DTOs.Companies;
using FinancialPortfolioManagement.Service.DTOs.Employees;
using FinancialPortfolioManagement.Service.Exceptions;
using FinancialPortfolioManagement.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialPortfolioManagement.Service.Services;

public class CompanyService : ICompanyService
{
    private readonly IRepository<Company> repository;
    private readonly IMapper mapper;
    public CompanyService(IRepository<Company> repository,IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<CompanyResultDto> AddAsync(CompanyCreationDto dto)
    {
        var company = await repository.GetAsync(x => x.Name.Equals(dto.Name));
        if (company is not null)
            throw new AlreadyExistException("Already exist!");

        var mapCompany = mapper.Map<Company>(dto);
        await repository.InsertAsync(mapCompany);
        await repository.SaveAsync();

        var res = mapper.Map<CompanyResultDto>(mapCompany);
        return res;
    }

    public async Task<CompanyResultDto> ModifyAsync(CompanyUpdateDto dto)
    {
        var company = await repository.GetAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapCompany = mapper.Map(dto, company);
        repository.Update(mapCompany);
        await repository.SaveAsync();

        var res = mapper.Map<CompanyResultDto>(mapCompany);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var company = await repository.GetAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Drop(company);
        await repository.SaveAsync();

        return true;
    }

    public async Task<CompanyResultDto> ReceiveByIdAsync(long id)
    {
        var company = await repository.GetAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException($"Could not find {id}");

        var mapCompany = mapper.Map<CompanyResultDto>(company);
        return mapCompany;
    }

    public async Task<IEnumerable<EmployeeResultDto>> ReceiveAllEmployeesAsync(long companyId)
    {
        Expression<Func<Company, bool>> companyExpression = b => b.Id.Equals(companyId);

        var company = await repository.GetAsync(companyExpression, new[] { "Employees" })
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<IEnumerable<EmployeeResultDto>>(company.Employees);
        return res;
    }

    public async Task<IEnumerable<CompanyResultDto>> ReceiveAllAsync()
    {
        var company = await repository.GetAll().ToListAsync();
        var mapCompany = mapper.Map<IEnumerable<CompanyResultDto>>(company);
        return mapCompany;
    }
}
