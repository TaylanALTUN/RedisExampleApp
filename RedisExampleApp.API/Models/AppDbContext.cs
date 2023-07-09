using Microsoft.EntityFrameworkCore;

namespace RedisExampleApp.API.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Kalem1", Price = 100 },
                new Product() { Id = 2, Name = "Kalem2", Price = 200 },
                new Product() { Id = 3, Name = "Kalem3", Price = 300 },
                new Product() { Id = 4, Name = "Kalem4", Price = 400 },
                new Product() { Id = 5, Name = "Kalem5", Price = 500 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
