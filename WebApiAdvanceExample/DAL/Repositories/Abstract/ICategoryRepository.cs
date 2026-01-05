using System.Linq.Expressions;
using WebApiAdvanceExample.Entities;

namespace WebApiAdvanceExample.DAL.Repositories.Abstract;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(Expression<Func<Category, bool>>? filter = null, params Expression<Func<Category, object>>[] includes);
    Task<List<Category>> GetPaginatedAsync(int page = 1, int size = 15, Expression<Func<Category, bool>>? filter = null, params Expression<Func<Category, object>>[] includes);
    Task<Category?> GetAsync(Expression<Func<Category, bool>> filter, bool tracking = false, params Expression<Func<Category, object>>[] includes);
    Task AddAsync(Category category);
    Task DeleteAsync(Guid id);
    Task SaveAsync();
}


