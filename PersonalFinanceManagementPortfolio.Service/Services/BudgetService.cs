using AutoMapper;
using FinancialPortfolioManagement.DAL.IRepositories;
using FinancialPortfolioManagement.Domain.Entities.Budgets;
using FinancialPortfolioManagement.Service.DTOs.Budgets;
using FinancialPortfolioManagement.Service.DTOs.Expenses;
using FinancialPortfolioManagement.Service.Exceptions;
using FinancialPortfolioManagement.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialPortfolioManagement.Service.Services;

public class BudgetService : IBudgetService
{
    private readonly IRepository<Budget> repository;
    private readonly IMapper mapper;
    public BudgetService(IRepository<Budget> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    public async Task<BudgetResultDto> AddAsync(BudgetCreationDto dto)
    {
        var budget = await repository.GetAsync(x => x.companyId.Equals(dto.companyId));
        if (budget is not null)
            throw new AlreadyExistException("Already exist!");
        
        var mapBudget = mapper.Map<Budget>(dto);
        mapBudget.NetIncome = mapBudget.TotalIncome - mapBudget.TotalExpenses;
        await repository.InsertAsync(mapBudget);
        await repository.SaveAsync();

        var res = mapper.Map<BudgetResultDto>(mapBudget);
        return res;
    }

    public async Task<BudgetResultDto> ModifyAsync(BudgetUpdateDto dto)
    {
        var budget = await repository.GetAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapBudget = mapper.Map(dto, budget);
        repository.Update(mapBudget);
        await repository.SaveAsync();

        var res = mapper.Map<BudgetResultDto>(mapBudget);
        return res;
    }
    public async Task<bool> RemoveAsync(long id)
    {
        var budget = await repository.GetAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Drop(budget);
        await repository.SaveAsync();

        return true;
    }
    public async Task<BudgetResultDto> ReceiveByIdAsync(long id)
    {
        var budget = await repository.GetAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException($"Could not find {id}");

        var mapBudget = mapper.Map<BudgetResultDto>(budget);
        return mapBudget;
    }

    public async Task<IEnumerable<ExpenseResultDto>> ReceiveAllExpensesAsync(long budgetId)
    {
        Expression<Func<Budget, bool>> budgetExpression = b => b.Id.Equals(budgetId);

        var budget = await repository.GetAsync(budgetExpression, new[] {"Expenses"})
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<IEnumerable<ExpenseResultDto>>(budget.Expenses);
        return res;
    }
    public async Task<IEnumerable<BudgetResultDto>> ReceiveAllAsync()
    {
        var budget = await repository.GetAll().ToListAsync();
        var res = mapper.Map<IEnumerable<BudgetResultDto>>(budget);
        return res;
    }
}
