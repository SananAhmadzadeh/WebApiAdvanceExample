using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiAdvanceExample.Core.DAL.Repositories.Concrete.EfCore;
using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.DAL.Repositories.Abstract;
using WebApiAdvanceExample.Entities;

namespace WebApiAdvanceExample.DAL.Repositories.Concrete.EfCore
{
    public class EfCategoryRepository : EfBaseRepository<Category, WebApiAdvanceExampleDbContext>, ICategoryRepository
    {
        public EfCategoryRepository(WebApiAdvanceExampleDbContext context) : base(context) { }
    }
}
