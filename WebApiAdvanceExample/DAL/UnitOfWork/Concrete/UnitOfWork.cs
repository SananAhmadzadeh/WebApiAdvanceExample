using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.DAL.Repositories.Abstract;
using WebApiAdvanceExample.DAL.Repositories.Concrete.EfCore;
using WebApiAdvanceExample.DAL.UnitOfWork.Abstract;

namespace WebApiAdvanceExample.DAL.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebApiAdvanceExampleDbContext _context;
        private readonly ICategoryRepository _categoryRepository;

        public UnitOfWork(WebApiAdvanceExampleDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository ?? new EfCategoryRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
