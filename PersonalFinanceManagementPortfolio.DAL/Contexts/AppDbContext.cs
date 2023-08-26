using Microsoft.EntityFrameworkCore;
using FinancialPortfolioManagement.Domain.Entities.Plans;
using FinancialPortfolioManagement.Domain.Entities.Budgets;
using FinancialPortfolioManagement.Domain.Entities.Expenses;
using FinancialPortfolioManagement.Domain.Entities.Companies;
using FinancialPortfolioManagement.Domain.Entities.Employees;
using System.Net.Mail;

namespace FinancialPortfolioManagement.DAL.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Plan> Plans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Budget>()
            .HasMany(x => x.Expenses)
            .WithOne(x => x.Budget)
            .HasForeignKey(x => x.budgetId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Company>()
            .HasMany(x => x.Employees)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
