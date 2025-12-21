using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.Entities;
using WebApiAdvanceExample.Entities.DTOs.ProductDTOs;
using WebApiAdvanceExample.Entities.Enums;

namespace WebApiAdvanceExample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly WebApiAdvanceExampleDbContext _context;

        public ProductsController(WebApiAdvanceExampleDbContext  context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> products = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{code:int}")]
        public async Task<IActionResult> GetProductById(int code)
        {
            Product? product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Code == code);

            if (product == null)
            {
                return NotFound($"{code} - nömrəli product yoxdu!");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == dto.CategoryId);
            if (category == null)
                return NotFound($"Category {dto.CategoryId} tapılmadı!");

            int nextCode = 1;
            var lastProduct = await _context.Products
                                            .OrderByDescending(p => p.Code)
                                            .FirstOrDefaultAsync();

            if (lastProduct != null)
                nextCode = lastProduct.Code + 1;

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Code = nextCode,
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Description = dto.Description ?? string.Empty,
                Price = dto.Price,
                DiscountPrice = dto.DiscountPrice ?? 0,
                Status = dto.Status ?? ProductStatus.Inactive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.Created, product);
        }

        [HttpPut("{code:int}")]
        public async Task<IActionResult> UpdateProduct(int code, UpdateProductDto dto)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Code == code);
            if(product == null)
            {
                return NotFound($"{code} - nömrəli product yoxdu!");
            }

            product.Name = dto.Name;
            product.Description = dto.Description ?? product.Description;
            product.Price = dto.Price;
            product.DiscountPrice = dto.DiscountPrice ?? product.DiscountPrice;
            product.Status = dto.Status ?? product.Status;

            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{code:int}")]
        public async Task<IActionResult> DeleteProduct(int code)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Code == code);

            if (product == null)
            {
                return NotFound($"{code} - nömrəli product yoxdu!");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
