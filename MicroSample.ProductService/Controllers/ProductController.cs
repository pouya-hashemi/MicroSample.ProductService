using MicroSample.ProductService.Dtos;
using MicroSample.ProductService.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroSample.ProductService.Controllers
{
    [Route("ProductService/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IAppDbContext _appDbContext;

        public ProductController(IAppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _appDbContext.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(f => f.Id == id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null ||
                String.IsNullOrWhiteSpace(productCreateDto.Name))
            {
                return BadRequest("Fill the required fields");
            }
            var product = new Product()
            {
                Name = productCreateDto.Name,
                StockQuantity = productCreateDto.StockQuantity,
            };
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetById", new { Id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductEditDto productEditDto)
        {
            if (productEditDto == null ||
               String.IsNullOrWhiteSpace(productEditDto.Name))
            {
                return BadRequest("Fill the required fields");
            }

            var product = await _appDbContext.Products.FirstOrDefaultAsync(f => f.Id == id);

            if (product is null)
                return NotFound();

            product.Name = productEditDto.Name;

            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(f => f.Id == id);

            if (product is null)
                return NotFound();

            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
