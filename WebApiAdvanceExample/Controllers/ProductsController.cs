using AutoMapper;
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
        private readonly IMapper _mapper;
        public ProductsController(WebApiAdvanceExampleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetAllProducts()
        {
            List<Product> products = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<List<GetProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProductById(Guid id)
        {
            Product? product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound($"{id} - nömrəli product yoxdu!");
            }

            return Ok(_mapper.Map<GetProductDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<GetProductDto>> CreateProduct(CreateProductDto dto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == dto.CategoryId);

            if (category == null)
                return NotFound($"Category {dto.CategoryId} tapılmadı!");

            Product product = _mapper.Map<Product>(dto);
           
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<GetProductDto>(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto dto)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"{id} - nömrəli product yoxdu!");
            }

            _mapper.Map(dto, product);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound($"{id} - nömrəli product yoxdu!");
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
