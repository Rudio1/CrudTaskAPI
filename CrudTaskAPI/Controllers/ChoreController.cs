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
        public async Task<ActionResult<IEnumerable<ChoreResponseDto>>> GetChores()
        {
            var chores = await _choreService.GetAllAsync();
            var result = chores.Select(chore => new ChoreResponseDto
            {
                Id = chore.Id,
                Name = chore.Name,
                Description = chore.Description,
                Active = chore.Active,
                IsCompleted = chore.isCompleted,
                CategoryId = chore.CategoryId,
                CategoryName = chore.Category?.Name
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChoreResponseDto>> GetChore(int id)
        {
            var chore = await _choreService.GetByIdAsync(id);

            if (chore == null)
            {
                return NotFound();
            }

            var result = new ChoreResponseDto
            {
                Id = chore.Id,
                Name = chore.Name,
                Description = chore.Description,
                Active = chore.Active,
                IsCompleted = chore.isCompleted,
                CategoryId = chore.CategoryId,
                CategoryName = chore.Category?.Name
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ChoreResponseDto>> PostChore([FromBody] ChoreCreateDto choreDto)
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

            var response = new ChoreResponseDto
            {
                Id = chore.Id,
                Name = chore.Name,
                Description = chore.Description,
                Active = chore.Active,
                IsCompleted = chore.isCompleted,
                CategoryId = chore.CategoryId,
                CategoryName = chore.Category?.Name
            };

            return CreatedAtAction(nameof(GetChore), new { id = chore.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ChoreResponseDto>> PutChore(int id, [FromBody] ChoreUpdateDto choreDto)
        {
            var existingChore = await _choreService.GetByIdAsync(id);
            if (existingChore == null)
            {
                return NotFound();
            }

            var duplicateChore = await _choreService.GetChoreIfExistsForUpdateAsync(id, choreDto.Name, existingChore.CategoryId);
            if (duplicateChore != null)
            {
                return BadRequest("A chore with the same name already exists in this category.");
            }

            existingChore.Name = choreDto.Name;
            existingChore.Description = choreDto.Description;
            existingChore.Active = choreDto.Active;
            existingChore.isCompleted = choreDto.IsCompleted;

            await _choreService.UpdateAsync(existingChore);

            var response = new ChoreResponseDto
            {
                Id = existingChore.Id,
                Name = existingChore.Name,
                Description = existingChore.Description,
                Active = existingChore.Active,
                IsCompleted = existingChore.isCompleted,
                CategoryId = existingChore.CategoryId,
                CategoryName = existingChore.Category?.Name
            };

            return Ok(response);
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
        public async Task<ActionResult<ChoreResponseDto>> CompleteTask(int id)
        {
            var task = await _choreService.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            task.isCompleted = true;
            await _choreService.UpdateAsync(task);

            var response = new ChoreResponseDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Active = task.Active,
                IsCompleted = task.isCompleted,
                CategoryId = task.CategoryId,
                CategoryName = task.Category?.Name
            };

            return Ok(response);
        }

        [HttpPost("uncomplete/{id}")]
        public async Task<ActionResult<ChoreResponseDto>> UncompleteTask(int id)
        {
            var chore = await _choreService.GetByIdAsync(id);
            if (chore == null)
            {
                return NotFound();
            }
            await _choreService.UncompleteTask(chore);

            var response = new ChoreResponseDto
            {
                Id = chore.Id,
                Name = chore.Name,
                Description = chore.Description,
                Active = chore.Active,
                IsCompleted = chore.isCompleted,
                CategoryId = chore.CategoryId,
                CategoryName = chore.Category?.Name
            };

            return Ok(response);
        }

        [HttpPost("reactive/{id}")]
        public async Task<ActionResult<ChoreResponseDto>> ReactiveTask(int id)
        {
            var chore = await _choreService.GetByIdAsync(id);
            if (chore == null)
            {
                return NotFound();
            }
            chore.Active = true;
            await _choreService.UpdateAsync(chore);

            var response = new ChoreResponseDto
            {
                Id = chore.Id,
                Name = chore.Name,
                Description = chore.Description,
                Active = chore.Active,
                IsCompleted = chore.isCompleted,
                CategoryId = chore.CategoryId,
                CategoryName = chore.Category?.Name
            };

            return Ok(response);
        }
    }
}
