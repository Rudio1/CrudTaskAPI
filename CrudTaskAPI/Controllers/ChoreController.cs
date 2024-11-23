using CrudTaskAPI.Application.Dto;
using CrudTaskAPI.Application.Interfaces;
using CrudTaskAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CrudTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoreController : ControllerBase
    {
        private readonly IChoreService _choreService;
        private readonly ICategoryService _categoryService;

        public ChoreController(IChoreService choreService, ICategoryService categoryService)
        {
            _choreService = choreService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chore>>> GetChores()
        {
            var chores = await _choreService.GetAllAsync();
            var result = chores.Select(chore => new
            {
                chore.Id,
                chore.Name,
                chore.Description,
                chore.Active,
                chore.isCompleted,
                category = chore.Category?.Name
            });

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Chore>> GetChore(int id)
        {
            var chore = await _choreService.GetByIdAsync(id);

            if (chore == null)
            {
                return NotFound();
            }

            var result = new
            {
                chore.Id,
                chore.Name,
                chore.Description,
                chore.Active,
                chore.isCompleted,
                category = chore.Category?.Name
            };

            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<Chore>> PostChore([FromBody] ChoreCreateDto choreDto)
        {
            int categoryId = choreDto.CategoryId;
            var categoryExists = await _categoryService.GetByIdAsync(categoryId);
            if (categoryExists == null)
            {
                return BadRequest("Category not found");
            }
            var existingChore = await _choreService.GetChoreIfExistsAsync(choreDto);

            if (existingChore != null)
            {
                return BadRequest("A chore with the same name already exists in this category.");
            }

            var chore = new Chore
            {
                Name = choreDto.Name,
                Description = choreDto.Description,
                Active = choreDto.Active,
                isCompleted = choreDto.IsCompleted,
                CategoryId = choreDto.CategoryId,
                Category = categoryExists
            };

            await _choreService.AddAsync(chore);
            return CreatedAtAction(nameof(GetChore), new { id = chore.Id }, chore);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutChore(int id, [FromBody] ChoreUpdateDto choreDto)
        {

            var existingChore = await _choreService.GetByIdAsync(id);
            if (existingChore == null)
            {
                return NotFound();
            }

            existingChore.Name = choreDto.Name;
            existingChore.Description = choreDto.Description;
            existingChore.Active = choreDto.Active;
            existingChore.isCompleted = choreDto.IsCompleted;

            await _choreService.UpdateAsync(existingChore);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChore(int id)
        {
            var task = await _choreService.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            await _choreService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("complete/{id}")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var task = await _choreService.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            task.isCompleted = true;
            await _choreService.UpdateAsync(task);
            return NoContent();
        }


        [HttpPost("uncomplete/{id}")]
        public async Task<IActionResult> UncompleteTask(int id)
        {
            var chore = await _choreService.GetByIdAsync(id);
            if (chore == null)
            {
                return NotFound();
            }
            await _choreService.UncompleteTask(chore);

            return NoContent();
        }


        [HttpPost("reactive/{id}")]
        public async Task<IActionResult> ReactiveTask(int id)
        {
            var chore = await _choreService.GetByIdAsync(id);
            if (chore == null)
            {
                return NotFound();
            }
            chore.Active = true;
            await _choreService.UpdateAsync(chore);

            return NoContent();
        }


    }
}
