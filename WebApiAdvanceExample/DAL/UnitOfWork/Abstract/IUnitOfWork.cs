using WebApiAdvanceExample.DAL.Repositories.Abstract;

namespace WebApiAdvanceExample.DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public Task SaveAsync();
    }
}
