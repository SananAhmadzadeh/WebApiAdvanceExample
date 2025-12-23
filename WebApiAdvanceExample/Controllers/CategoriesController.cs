using AutoMapper;
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
        private readonly IMapper _mapper;
        public CategoriesController(WebApiAdvanceExampleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCategoryDto>>> GetAllCategories()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<List<GetCategoryDto>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(Guid id)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound($"{id} - nömrəli kateqoriya tapılmadı!");

            return Ok(_mapper.Map<GetCategoryDto>(category));
        }

        [HttpPost]
        public async Task<ActionResult<GetCategoryDto>> CreateCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<GetCategoryDto>(category);

            return CreatedAtAction(nameof(GetCategoryById) , new { id = category.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound($"{id} - nömrəli kateqoriya tapılmadı!");

            _mapper.Map(dto, category);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound($"{id} - nömrəli kateqoriya yoxdu!");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}