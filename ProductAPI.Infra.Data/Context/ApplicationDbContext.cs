using ProductAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Infra.Data.Mapping;

namespace ProductAPI.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Brand> Brand { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(new ProductMap().Configure);
            modelBuilder.Entity<Brand>(new BrandMap().Configure);
        }

    }
}
