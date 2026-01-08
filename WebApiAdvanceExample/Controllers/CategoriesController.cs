using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAdvanceExample.DAL.UnitOfWork.Abstract;
using WebApiAdvanceExample.Entities;
using WebApiAdvanceExample.Entities.DTOs.CategoryDTOs;

namespace WebApiAdvanceExample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<List<GetCategoryDto>>> GetAllCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            return Ok(_mapper.Map<List<GetCategoryDto>>(categories));
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCategoryDto>>> GetAllCategoriesPaginated()
        {
            var categories = await _unitOfWork.CategoryRepository.GetPaginatedAsync();

            return Ok(_mapper.Map<List<GetCategoryDto>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);

            if (category == null)
                return NotFound($"{id} - nömrəli kateqoriya tapılmadı!");

            return Ok(_mapper.Map<GetCategoryDto>(category));
        }

        [HttpPost]
        public async Task<ActionResult<GetCategoryDto>> CreateCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();

            var result = _mapper.Map<GetCategoryDto>(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id, tracking: true);

            if (category == null)
                return NotFound($"{id} - nömrəli kateqoriya tapılmadı!");

            _mapper.Map(dto, category);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);

            if (category == null)
                return NotFound($"{id} - nömrəli kateqoriya yoxdu!");

            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}