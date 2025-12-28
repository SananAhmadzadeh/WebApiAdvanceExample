using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApiAdvanceExample.Entities;
using WebApiAdvanceExample.Entities.Auth;

namespace WebApiAdvanceExample.DAL.EFCore
{
    public class WebApiAdvanceExampleDbContext : IdentityDbContext<AppUser<Guid>> 
    {
        public WebApiAdvanceExampleDbContext(DbContextOptions<WebApiAdvanceExampleDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
