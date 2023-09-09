using Example.Api.Data.Context;
using Example.Api.Dto;
using Example.Api.Entity;
using Gleeman.EffectiveMapper.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IEffectiveMapper _effectiveMapper;

        public ProductsController(IEffectiveMapper effectiveMapper)
        {
            _effectiveMapper = effectiveMapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GetProductsDto>> GetProducts()
        {
            using AppDbContext dbContext = new();
            IEnumerable<Product> products = dbContext.Products.ToList();
            IEnumerable<GetProductsDto> productsDto = _effectiveMapper.Map<GetProductsDto, Product>(products,p=> new GetProductsDto
            {
                ProductId = p.Id,
                ProductPrice = p.Price,
                ProductName = p.ProductName,
                ProductQuantity = p.Quantity
            });
            return Ok(productsDto);
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

        [HttpPut("{id}")]
        public ActionResult CreateProduct(int id,UpdateProductDto updateProduct)
        {
            using AppDbContext dbContext = new();
            var existProduct = dbContext.Products.SingleOrDefault(p => p.Id == id)!;
            if(existProduct == null)
            {
                return NotFound();
            }

            existProduct = _effectiveMapper.Map<Product, UpdateProductDto>(updateProduct);
            dbContext.Entry(existProduct).State = EntityState.Modified;
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
