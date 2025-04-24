using CrudTaskAPI.Application.Dto;
using CrudTaskAPI.Application.Interfaces;
using CrudTaskAPI.Domain.Entities;
using CrudTaskAPI.Domain.Enums;
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

        private ChoreResponseDto MapToResponseDto(Chore chore)
        {
            return new ChoreResponseDto
            {
                Id = chore.Id,
                Name = chore.Name,
                Description = chore.Description,
                Active = chore.Active,
                IsCompleted = chore.isCompleted,
                CategoryId = chore.CategoryId,
                CategoryName = chore.Category?.Name,
                Progress = chore.Progress,
                CreatedAt = chore.CreatedAt
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChoreResponseDto>>> GetChores()
        {
            var chores = await _choreService.GetAllAsync();
            var result = chores.Select(MapToResponseDto);
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

            return Ok(MapToResponseDto(chore));
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
                Category = categoryExists,
                Progress = choreDto.Progress
            };

            await _choreService.AddAsync(chore);
            return CreatedAtAction(nameof(GetChore), new { id = chore.Id }, MapToResponseDto(chore));
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
            existingChore.Progress = choreDto.Progress;

            await _choreService.UpdateAsync(existingChore);
            return Ok(MapToResponseDto(existingChore));
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
            task.Progress = ChoreProgressEnum.Done;
            await _choreService.UpdateAsync(task);

            return Ok(MapToResponseDto(task));
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

            return Ok(MapToResponseDto(chore));
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

            return Ok(MapToResponseDto(chore));
        }

        [HttpPost("progress/do/{id}")]
        public async Task<ActionResult<ChoreResponseDto>> SetProgressToDo(int id)
        {
            var chore = await _choreService.GetByIdAsync(id);
            if (chore == null)
            {
                return NotFound();
            }

            chore.Progress = ChoreProgressEnum.ToDo;
            await _choreService.UpdateAsync(chore);

            return Ok(MapToResponseDto(chore));
        }

        [HttpPost("progress/doing/{id}")]
        public async Task<ActionResult<ChoreResponseDto>> SetProgressDoing(int id)
        {
            var chore = await _choreService.GetByIdAsync(id);
            if (chore == null)
            {
                return NotFound();
            }

            chore.Progress = ChoreProgressEnum.Doing;
            await _choreService.UpdateAsync(chore);

            return Ok(MapToResponseDto(chore));
        }

        [HttpPost("progress/done/{id}")]
        public async Task<ActionResult<ChoreResponseDto>> SetProgressDone(int id)
        {
            var chore = await _choreService.GetByIdAsync(id);
            if (chore == null)
            {
                return NotFound();
            }

            chore.Progress = ChoreProgressEnum.Done;
            await _choreService.UpdateAsync(chore);

            return Ok(MapToResponseDto(chore));
        }
    }
}
