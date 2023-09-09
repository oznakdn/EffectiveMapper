using Example.ConsoleApp.Dto;
using Example.ConsoleApp.Entity;
using Gleeman.EffectiveMapper.Mapper;


// Entity instance
Product product = new()
{
    Name = "TestProduct",
    Price = 10

};


var products = new List<Product>()
{
    new Product
    {
        Name = "ProductOne",
        Price = 100
    },
    new Product
    {
        Name = "ProductTwo",
        Price = 200
    }
};


// Dto instance
ProductDto productDto = new()
{
    ProductName = "TestDto",
    ProductPrice = 20
};

var productsDto = new List<ProductDto>()
{
    new ProductDto
    {
        ProductName= "ProductOneDto",
        ProductPrice = 20
    },
    new ProductDto
    {
        ProductName= "ProductTwoDto",
        ProductPrice = 30
    }

};



IEffectiveMapper mapper = new EffectiveMapper();

ProductDto dto = mapper.Map<ProductDto, Product>(product, map => new ProductDto
{
    ProductName = product.Name,
    ProductPrice = product.Price
});
Console.WriteLine($"ProductDto: {dto.ProductName} - {dto.ProductPrice}");


Product prod = mapper.Map<Product, ProductDto>(productDto, x => new Product
{
    Name = x.ProductName,
    Price = x.ProductPrice
});
Console.WriteLine($"Product: {prod.Name} - {prod.Price}");


IEnumerable<ProductDto> Dtos = mapper.Map<ProductDto, Product>(products, x => new ProductDto
{
    ProductName = product.Name,
    ProductPrice = product.Price
});
foreach (var item in Dtos)
{
    Console.WriteLine(item.ProductName + " " + item.ProductPrice);
}


IEnumerable<Product> productsList = mapper.Map<Product, ProductDto>(productsDto, x => new Product
{
    Name = x.ProductName,
    Price = x.ProductPrice
});

foreach (var item in productsList)
{
    Console.WriteLine(item.Name + " " + item.Price);
}
