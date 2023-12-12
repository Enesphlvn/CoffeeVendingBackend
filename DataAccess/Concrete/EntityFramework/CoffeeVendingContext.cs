using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class CoffeeVendingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseNpgsql("Host=localhost; Database=CoffeeVending; Username=postgres; Password=12345");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductContent> ProductContents { get; set; }
        public DbSet<GeneralContent> GeneralContents { get; set; }
    }
}