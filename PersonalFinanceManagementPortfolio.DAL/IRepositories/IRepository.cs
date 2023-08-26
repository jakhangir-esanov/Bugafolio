using FinancialPortfolioManagement.Domain.Commons;
using System.Linq.Expressions;

namespace FinancialPortfolioManagement.DAL.IRepositories;

public interface IRepository<T> where T : Auditable
{
    Task InsertAsync(T entity);
    void Update(T entity);
    void Drop(T entity);
    Task<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null);
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null!, bool isNoTracking = true, string[] includes = null!);
    Task SaveAsync();
}