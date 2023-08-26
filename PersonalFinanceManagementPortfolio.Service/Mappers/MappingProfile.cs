using AutoMapper;
using FinancialPortfolioManagement.Service.DTOs.Plans;
using FinancialPortfolioManagement.Service.DTOs.Budgets;
using FinancialPortfolioManagement.Service.DTOs.Expenses;
using FinancialPortfolioManagement.Domain.Entities.Plans;
using FinancialPortfolioManagement.Service.DTOs.Companies;
using FinancialPortfolioManagement.Service.DTOs.Employees;
using FinancialPortfolioManagement.Domain.Entities.Budgets;
using FinancialPortfolioManagement.Domain.Entities.Expenses;
using FinancialPortfolioManagement.Domain.Entities.Employees;
using FinancialPortfolioManagement.Domain.Entities.Companies;

namespace FinancialPortfolioManagement.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Budget
        CreateMap<Budget, BudgetCreationDto>().ReverseMap();
        CreateMap<BudgetUpdateDto, Budget>().ReverseMap();
        CreateMap<BudgetResultDto, Budget>().ReverseMap();

        //Company
        CreateMap<Company, CompanyCreationDto>().ReverseMap();
        CreateMap<CompanyUpdateDto, Company>().ReverseMap();
        CreateMap<CompanyResultDto, Company>().ReverseMap();

        // Employee
        CreateMap<Employee, EmployeeCreationDto>().ReverseMap();
        CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();
        CreateMap<EmployeeResultDto, Employee>().ReverseMap();

        // Expense 
        CreateMap<Expense, ExpenseCreationDto>().ReverseMap();
        CreateMap<ExpenseUpdateDto, Expense>().ReverseMap();
        CreateMap<ExpenseResultDto, Expense>().ReverseMap();

        // Plan
        CreateMap<Plan, PlanCreationDto>().ReverseMap();
        CreateMap<PlanUpdateDto, Plan>().ReverseMap();
        CreateMap<PlanResultDto, Plan>().ReverseMap();
    }
}
