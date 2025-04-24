using CrudTaskAPI.Application.Dto;
using CrudTaskAPI.Application.Interfaces;
using CrudTaskAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CrudTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            var result = categories.Select(category => new
            {
                category.Id,
                category.Name
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var result = new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Chores = category.Chores?.Select(c => new ChoreResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Active = c.Active,
                    IsCompleted = c.isCompleted,
                    CategoryId = c.CategoryId,
                    CategoryName = c.Category?.Name
                }).ToList()
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponseDto>> PostCategory([FromBody] CategoryCreateDto categoryDto)
        {
            var category = await _categoryService.AddAsync(categoryDto);
            var response = new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Chores = new List<ChoreResponseDto>()
            };

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> PutCategory(int id, [FromBody] CategoryUpdateDto categoryDto)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryDto.Name;
            await _categoryService.UpdateAsync(category);

            var response = new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Chores = category.Chores?.Select(c => new ChoreResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Active = c.Active,
                    IsCompleted = c.isCompleted,
                    CategoryId = c.CategoryId,
                    CategoryName = c.Category?.Name
                }).ToList()
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
