using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApiAdvanceExample.Entities;

namespace WebApiAdvanceExample.DAL.EFCore
{
    public class WebApiAdvanceExampleDbContext : DbContext
    {
        public WebApiAdvanceExampleDbContext(DbContextOptions<WebApiAdvanceExampleDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
