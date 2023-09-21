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

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, UpdateProductDto updateProduct)
        {

            var existProduct = _dbContext.Products.SingleOrDefault(p => p.Id == id)!;
            if (existProduct == null)
            {
                return NotFound();
            }

            existProduct = _effectiveMapper.Map<Product, UpdateProductDto>(updateProduct);
            _dbContext.Entry(existProduct).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
