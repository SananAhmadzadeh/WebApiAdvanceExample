using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.DAL.Repositories.Abstract;
using WebApiAdvanceExample.Entities;

namespace WebApiAdvanceExample.DAL.Repositories.Concrete.EfCore
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private readonly WebApiAdvanceExampleDbContext _context;
        public EfCategoryRepository(WebApiAdvanceExampleDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
                _context.Categories.Remove(category);
        }

        public async Task<List<Category>> GetAllAsync(Expression<Func<Category, bool>>? filter = null, params Expression<Func<Category, object>>[] includes)
        {
            IQueryable<Category> query = CreateQuery(false, includes);

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<List<Category>> GetPaginatedAsync(int page, int size, Expression<Func<Category, bool>>? filter = null, params Expression<Func<Category, object>>[] includes)
        {
            IQueryable<Category> query = CreateQuery(false, includes);

            if (filter != null)
                query = query.Where(filter);

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        } 

        public async Task<Category?> GetAsync( Expression<Func<Category, bool>> filter, bool tracking = false, params Expression<Func<Category, object>>[] includes)
        {
            IQueryable<Category> query = CreateQuery(tracking, includes);
            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private IQueryable<Category> CreateQuery(bool tracking, params Expression<Func<Category, object>>[] includes)
        {
            IQueryable<Category> query = tracking
                ? _context.Categories
                : _context.Categories.AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return query;
        }

    }
}
