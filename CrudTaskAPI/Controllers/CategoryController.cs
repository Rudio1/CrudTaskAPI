using CrudTaskAPI.Application.Dto;
using CrudTaskAPI.Application.Interfaces;
using CrudTaskAPI.Infra.Infrastructure.Data;
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
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
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
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryCreateDto categoryDto)
        {
            var category = await _categoryService.AddAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryUpdateDto category)
        {
            var categoryExists = await _categoryService.GetByIdAsync(id);
            if (categoryExists == null)
            {
                return NotFound();
            }

            categoryExists.Name = category.Name;
            await _categoryService.UpdateAsync(categoryExists);

            return NoContent();
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
