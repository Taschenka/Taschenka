using Microsoft.AspNetCore.Mvc;
using Taschenka.Dtos;
using Taschenka.Services.Interfaces;

namespace Taschenka.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodosService _todosService;

    public TodosController(ITodosService todosService)
    {
        _todosService = todosService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTodoDto>>> GetAllTodosAsync()
    {
        var todos = await _todosService.GetAllTodosAsync();

        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTodoDto?>> GetTodoByIdAsync(Guid id)
    {
        var todo = await _todosService.GetTodoByIdAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<GetTodoDto>> CreateTodoAsync(CreateTodoDto todoDto)
    {
        var created = await _todosService.CreateTodoAsync(todoDto);

        return CreatedAtAction(nameof(GetTodoByIdAsync), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTodoAsync(Guid id, UpdateTodoDto todoDto)
    {
        var success = await _todosService.UpdateTodoAsync(id, todoDto);

        if (!success)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodoAsync(Guid id)
    {
        var success = await _todosService.DeleteTodoAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
