using Example.Api.Data.Context;
using Example.Api.Entity;

namespace Example.Api.Data.Database;

public class DatabaseInitializer
{
    public static void DataInitialize(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<AppDbContext>();

        db.Products.AddRange(new List<Product>
        {
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
            }
        });

       

        db.SaveChanges();
    }
}
