using Example.Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Example.Api.Data.Context;

public class AppDbContext : DbContext
{
    private const string ConnectionString = "Data Source = C:\\Users\\USER\\Desktop\\Gleeman.EffectiveMapper\\EffectiveMapper\\example\\Example.Api\\Data\\Database\\TestDb.db";
    public AppDbContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<Product>Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasData(
            new Product
            {
                Id = 1,
                ProductName = "Test1",
                Price = 100,
                Quantity = 10
            },
            new Product
            {
                Id = 2,
                ProductName = "Test2",
                Price = 200,
                Quantity = 20
            },
            new Product
            {
                Id = 3,
                ProductName = "Test3",
                Price = 300,
                Quantity = 30
            },
            new Product
            {
                Id = 4,
                ProductName = "Test4",
                Price = 400,
                Quantity = 40
            },
            new Product
            {
                Id = 5,
                ProductName = "Test5",
                Price = 500,
                Quantity = 50
            });
    }
}
