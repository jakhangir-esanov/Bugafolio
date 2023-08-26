using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FinancialPortfolioManagement.DAL.IRepositories;
using FinancialPortfolioManagement.Service.Exceptions;
using FinancialPortfolioManagement.Service.Interfaces;
using FinancialPortfolioManagement.Service.DTOs.Employees;
using FinancialPortfolioManagement.Domain.Entities.Employees;
using FinancialPortfolioManagement.Service.Securities;

namespace FinancialPortfolioManagement.Service.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> repository;
    private readonly IMapper mapper;
    public EmployeeService(IRepository<Employee> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<EmployeeResultDto> AddAsync(EmployeeCreationDto dto)
    {
        var employee = await repository.GetAsync(x => x.Email.Equals(dto.Email));
        if (employee is not null)
            throw new AlreadyExistException("Already exist!");

        var hashResult = PasswordHasher.Hash(dto.Password);
        
        var mapEmployee = mapper.Map<Employee>(dto);
        mapEmployee.Password = hashResult.Password;
        mapEmployee.Salt = hashResult.Salt;
        await repository.InsertAsync(mapEmployee);
        await repository.SaveAsync();

        var res = mapper.Map<EmployeeResultDto>(mapEmployee);
        return res;
    }

    public async Task<EmployeeResultDto> ModifyAsync(EmployeeUpdateDto dto)
    {
        var employee = await repository.GetAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var hashResult = PasswordHasher.Hash(dto.Password);

        var mapEmployee = mapper.Map(dto, employee);
        mapEmployee.Password = hashResult.Password;
        mapEmployee.Salt = hashResult.Salt;
        repository.Update(mapEmployee);
        await repository.SaveAsync();

        var res = mapper.Map<EmployeeResultDto>(mapEmployee);
        return res;
    }
    public async Task<bool> RemoveAsync(long id)
    {
        var employee = await repository.GetAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Drop(employee);
        await repository.SaveAsync();

        return true;
    }
    public async Task<EmployeeResultDto> ReceiveByIdAsync(long id)
    {
        var employee = await repository.GetAsync(x => x.Id.Equals(id))
           ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<EmployeeResultDto>(employee);
        return res;
    }

    public async Task<IEnumerable<EmployeeResultDto>> ReceiveAllAsync()
    {
        var employee = await repository.GetAll().ToListAsync();
        var res = mapper.Map<IEnumerable<EmployeeResultDto>>(employee);
        return res;
    }
}
