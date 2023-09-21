using Example.Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Example.Api.Data.Context;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
   
    public DbSet<Product>Products { get; set; }

}
