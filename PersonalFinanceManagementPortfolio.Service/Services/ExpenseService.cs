using AutoMapper;
using FinancialPortfolioManagement.DAL.IRepositories;
using FinancialPortfolioManagement.Service.Exceptions;
using FinancialPortfolioManagement.Service.Interfaces;
using FinancialPortfolioManagement.Service.DTOs.Expenses;
using FinancialPortfolioManagement.Domain.Entities.Budgets;
using FinancialPortfolioManagement.Domain.Entities.Expenses;
using Microsoft.EntityFrameworkCore;

namespace FinancialPortfolioManagement.Service.Services;

public class ExpenseService : IExpenseService
{
    private readonly IRepository<Expense> repository;
    private readonly IRepository<Budget> repository1;
    private readonly IMapper mapper;
    public ExpenseService(IRepository<Expense> repository,IRepository<Budget> repository1, IMapper mapper)
    {
        this.repository = repository;
        this.repository1 = repository1;
        this.mapper = mapper;
    }

    public async Task<ExpenseResultDto> AddAsync(ExpenseCreationDto dto)
    {
        var expense = await repository.GetAsync(x => x.budgetId.Equals(dto.budgetId));
        if (expense is not null)
            throw new AlreadyExistException("Already exist!");

        var budget = await repository1.GetAsync(x => x.Id.Equals(dto.budgetId));

        if (0 < (budget.TotalExpenses - expense.ExitAmount))
        {
            var mapExpense = mapper.Map<Expense>(dto);
            budget.TotalExpenses -= mapExpense.ExitAmount;
            budget.TotalIncome += mapExpense.EnterAmount;
            await repository.InsertAsync(mapExpense);
            await repository.SaveAsync();

            var res = mapper.Map<ExpenseResultDto>(mapExpense);
            return res;
        }
        else
            throw new InsufficientAmount("Amount is not sufficient!");
    }

    public async Task<ExpenseResultDto> ModifyAsync(ExpenseUpdateDto dto)
    {
        var expense = await repository.GetAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapExpense = mapper.Map(dto, expense);
        repository.Update(mapExpense);
        await repository.SaveAsync();

        var res = mapper.Map<ExpenseResultDto>(mapExpense);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var expense = await repository.GetAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Drop(expense);
        await repository.SaveAsync();

        return true;
    }

    public async Task<ExpenseResultDto> ReceiveByIdAsync(long id)
    {
        var expense = await repository.GetAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException($"Could not receive id {id}");

        var res = mapper.Map<ExpenseResultDto>(expense);
        return res;
    }

    public async Task<IEnumerable<ExpenseResultDto>> ReceiveAllAsync()
    {
        var expense = await repository.GetAll().ToListAsync();
        var res = mapper.Map<IEnumerable<ExpenseResultDto>>(expense);
        return res;
    }
}
