using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiAdvanceExample.Core.DAL.Repositories.Abstract;
using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.Entities;

namespace WebApiAdvanceExample.Core.DAL.Repositories.Concrete.EfCore
{
    public abstract class EfBaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _entities;
        public EfBaseRepository(TContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();

        }
        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _entities.FindAsync(id);

            if (category != null)
                _entities.Remove(category);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = CreateQuery(false, includes);

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>>? filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = CreateQuery(false, includes);

            if (filter != null)
                query = query.Where(filter);

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracking = false, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = CreateQuery(tracking, includes);
            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private IQueryable<TEntity> CreateQuery(bool tracking, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = tracking
                ? _entities
                : _entities.AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return query;
        }
    }
}
