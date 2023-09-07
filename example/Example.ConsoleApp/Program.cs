using Example.ConsoleApp.Dto;
using Example.ConsoleApp.Entity;
using Gleeman.EffectiveMapper.Mapper;

Product product = new()
{
    Name = "TestProduct",
    Age = 10

};


var products = new List<Product>()
{
    new Product
    {
        Name = "ProductOne",
        Age = 100
    },
    new Product
    {
        Name = "ProductTwo",
        Age = 200
    }
};

ProductDto productDto = new()
{
    Name = "TestDto",
    Age = 20
};



IEffectiveMapper mapper = new EffectiveMapper();

ProductDto dto = mapper.Map<ProductDto, Product>(product);
Console.WriteLine($"ProductDto: {dto.Name} - {dto.Age}");


Product prod = mapper.Map<Product, ProductDto>(productDto);
Console.WriteLine($"Product: {prod.Name} - {prod.Age}");


IEnumerable<ProductDto> productsDto = mapper.Map<ProductDto, Product>(products);
foreach (var item in productsDto)
{
    Console.WriteLine(item.Name + " " + item.Age);
}
