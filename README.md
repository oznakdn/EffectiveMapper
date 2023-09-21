# Gleeman Effective Mapper

`dotnet` CLI
```
> dotnet add package Gleeman.EffectiveMapper --version 1.0.0
```
## How to use?

### When the entity and dto properties are the same.

```csharp
// Entity
public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
```
```csharp
// Dtos
public class GetProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class GetProductsDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class CreateProductDto
{
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
    private readonly AppDbContext _dbContext;
    public ProductsController(IEffectiveMapper effectiveMapper, AppDbContext dbContext)
    {
        _effectiveMapper = effectiveMapper;
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<GetProductsDto>> GetProducts()
    {
        IEnumerable<Product> products = _dbContext.Products.ToList();
        IEnumerable<GetProductsDto> productsDto = _effectiveMapper.Map<GetProductsDto, Product>(products);
        return Ok(productsDto);
    }

    [HttpGet("{id}")]
    public ActionResult<GetProductDto> GetProduct(int id)
    {
        Product product = _dbContext.Products.SingleOrDefault(p => p.Id == id)!;
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
        if (ModelState.IsValid)
        {
            Product product = _effectiveMapper.Map<Product, CreateProductDto>(createProduct);
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return Created("", createProduct);
        }
        return BadRequest(ModelState.Select(e => e.Value.Errors).ToList());
    }
}
```

### When the entity and dto properties are different.

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
public class GetProductsDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductQuantity { get; set; }
}

public class CreateProductDto
{
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductQuantity { get; set; }
}
```
```csharp
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IEffectiveMapper _effectiveMapper;
    private readonly AppDbContext _dbContext;
    public ProductsController(IEffectiveMapper effectiveMapper, AppDbContext dbContext)
    {
        _effectiveMapper = effectiveMapper;
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<GetProductsDto>> GetProducts()
    {
        IEnumerable<Product> products = _dbContext.Products.ToList();
        IEnumerable<GetProductsDto> productsDto = _effectiveMapper.Map<GetProductsDto, Product>(products,p=> new GetProductsDto
        {
            ProductId = p.Id,
            ProductName = p.ProductName,
            ProductPrice = p.Price,
            ProductQuantity = p.Quantity
        });
        return Ok(productsDto);
    }

    [HttpPost]
    public ActionResult<GetProductDto> CreateProduct(CreateProductDto createProduct)
    {
        if (ModelState.IsValid)
        {
            Product product = _effectiveMapper.Map<Product, CreateProductDto>(createProduct,p=> new Product
            {
               ProductName = p.ProductName,
               Price = p.ProductPrice,
               Quantity = p.ProductQuantity
            });
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return Created("", createProduct);
        }
        return BadRequest(ModelState.Select(e => e.Value.Errors).ToList());
    }
}
```
