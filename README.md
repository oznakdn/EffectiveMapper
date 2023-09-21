# Gleeman Effective Mapper

`dotnet` CLI
```
> dotnet add package Gleeman.EffectiveMapper --version 1.0.0
```
## How to use?

### If the entity and dto properties are not different.

```csharp
public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
```
```csharp
public class GetProductDto
{
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
}
```
```csharp
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IEffectiveMapper _effectiveMapper;

        public ProductsController(IEffectiveMapper effectiveMapper)
        {
            _effectiveMapper = effectiveMapper;
        }

        [HttpGet("{id}")]
        public ActionResult<GetProductDto> GetProduct(int id)
        {
            using AppDbContext dbContext = new();
            Product product = dbContext.Products.SingleOrDefault(p => p.Id == id)!;
            if (product != null)
            {
                GetProductDto productDto = _effectiveMapper.Map<GetProductDto, Product>(product);
                return Ok(productDto);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<GetProductDto> CreateProduct(CreateProductDto createProduct)
        {
            using AppDbContext dbContext = new();
            Product product = _effectiveMapper.Map<Product, CreateProductDto>(createProduct);
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            return Created("",createProduct);
        }
    }
```
