using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.DAL.Repositories.Abstract;
using WebApiAdvanceExample.Entities;
using WebApiAdvanceExample.Entities.DTOs.CategoryDTOs;

namespace WebApiAdvanceExample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return Ok(_mapper.Map<List<GetCategoryDto>>(categories));
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCategoryDto>>> GetAllCategoriesPaginatedAsync()
        {
            var categories = await _categoryRepository.GetPaginatedAsync();

            return Ok(_mapper.Map<List<GetCategoryDto>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.GetAsync(c => c.Id == id);

            if (category == null)
                return NotFound($"{id} - nömrəli kateqoriya tapılmadı!");

            return Ok(_mapper.Map<GetCategoryDto>(category));
        }

        [HttpPost]
        public async Task<ActionResult<GetCategoryDto>> CreateCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();

            var result = _mapper.Map<GetCategoryDto>(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetAsync(c => c.Id == id, tracking: true);

            if (category == null)
                return NotFound($"{id} - nömrəli kateqoriya tapılmadı!");

            _mapper.Map(dto, category);

            await _categoryRepository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryRepository.GetAsync(c => c.Id == id);

            if (category == null)
                return NotFound($"{id} - nömrəli kateqoriya yoxdu!");

            await _categoryRepository.DeleteAsync(id);
            await _categoryRepository.SaveAsync();

            return NoContent();
        }
    }
}