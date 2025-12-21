using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.Entities;
using WebApiAdvanceExample.Entities.DTOs.CategoryDTOs;
using WebApiAdvanceExample.Entities.Enums;

namespace WebApiAdvanceExample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly WebApiAdvanceExampleDbContext _context;
        public CategoriesController(WebApiAdvanceExampleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            List<Category> categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{code:int}")]
        public async Task<IActionResult> GetCategoryById(int code)
        {
            Category? category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Code == code);
            if (category == null)
                return NotFound($"{code} - nömrəli kateqoriya yoxdu!");

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            int nextCode = 1;

            var lastCategory = await _context.Categories
                                             .OrderByDescending(c => c.Code)
                                             .FirstOrDefaultAsync();

            if (lastCategory != null)
                nextCode = lastCategory.Code + 1;

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Code = nextCode,
                Name = dto.Name,
                Description = dto.Description ?? string.Empty,
                Status = dto.Status ?? CategoryStatus.Active,
                Products = new List<Product>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.Created, category);
        }

        [HttpPut("{code:int}")]
        public async Task<IActionResult> UpdateCategory(int code, UpdateCategoryDto dto)
        {
            Category? updatedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Code == code);
            if (updatedCategory == null)
                return NotFound($"{code} - nömrəli kateqoriya yoxdu!");

            updatedCategory.Name = dto.Name;
            updatedCategory.Description = dto.Description ?? updatedCategory.Description;
            updatedCategory.Status = dto.Status ?? updatedCategory.Status;
            updatedCategory.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{code:int}")]
        public async Task<IActionResult> DeleteCategory(int code)
        {
            Category? deletedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Code == code);
            if (deletedCategory == null)
                return NotFound($"{code} - nömrəli kateqoriya yoxdu!");

            _context.Categories.Remove(deletedCategory);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
