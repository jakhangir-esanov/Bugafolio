using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FinancialPortfolioManagement.DAL.IRepositories;
using FinancialPortfolioManagement.Service.Interfaces;
using FinancialPortfolioManagement.Service.DTOs.Plans;
using FinancialPortfolioManagement.Service.Exceptions;
using FinancialPortfolioManagement.Domain.Entities.Plans;

namespace FinancialPortfolioManagement.Service.Services;

public class PlanService : IPlanService
{
    private readonly IRepository<Plan> repository;
    private readonly IMapper mapper;
    public PlanService(IRepository<Plan> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<PlanResultDto> AddAsync(PlanCreationDto dto)
    {
        var plan = await repository.GetAsync(x => x.Title.Equals(dto.Title));
        if (plan is not null)
            throw new AlreadyExistException("Already exist!");

        var mapPlan = mapper.Map<Plan>(dto);
        await repository.InsertAsync(mapPlan);
        await repository.SaveAsync();

        var res = mapper.Map<PlanResultDto>(mapPlan);
        return res;
    }

    public async Task<PlanResultDto> ModifyAsync(PlanUpdateDto dto)
    {
        var plan = await repository.GetAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapPlan = mapper.Map(dto, plan);
        repository.Update(mapPlan);
        await repository.SaveAsync();

        var res = mapper.Map<PlanResultDto>(mapPlan);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var plan = await repository.GetAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Drop(plan);
        await repository.SaveAsync();

        return true;
    }

    public async Task<PlanResultDto> ReceiveByIdAsync(long id)
    {
        var plan = await repository.GetAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException($"Could not find id {id}");

        var res = mapper.Map<PlanResultDto>(plan);
        return res;
    }

    public async Task<IEnumerable<PlanResultDto>> ReceiveAllAsync()
    {
        var plan = await repository.GetAll().ToListAsync();
        var res = mapper.Map<IEnumerable<PlanResultDto>>(plan);
        return res;
    }
}
