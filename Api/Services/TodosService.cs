using AutoMapper;
using Taschenka.Dtos;
using Taschenka.Entities;
using Taschenka.Repositories.Interfaces;
using Taschenka.Services.Interfaces;

namespace Taschenka.Services;

public class TodosService : ITodosService
{
    private readonly ITodosRepository _todosRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TodosService> _logger;

    public TodosService
    (
        ITodosRepository todosRepository,
        IMapper mapper,
        ILogger<TodosService> logger
    )
    {
        _todosRepository = todosRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetTodoDto>> GetAllTodosAsync()
    {
        _logger.LogDebug("GetAll has been called.");

        var todos = await _todosRepository.GetAllTodosAsync();

        return _mapper.Map<IEnumerable<GetTodoDto>>(todos);
    }

    public async Task<GetTodoDto?> GetTodoByIdAsync(Guid id)
    {
        _logger.LogDebug($"GetById has been called with id: {id}.");

        var todo = await _todosRepository.GetTodoByIdAsync(id);

        if (todo is null)
        {
            _logger.LogDebug($"Todo not found with id: {id}.");
        }

        return _mapper.Map<GetTodoDto?>(todo);
    }

    public async Task<GetTodoDto> CreateTodoAsync(CreateTodoDto todoDto)
    {
        _logger.LogDebug($"CreateTodo has been called with todoDto: {todoDto}.");

        var todo = _mapper.Map<Todo>(todoDto);

        await _todosRepository.CreateTodoAsync(todo);

        var created = _mapper.Map<GetTodoDto>(todo);

        return created;
    }

    public async Task<bool> UpdateTodoAsync(Guid id, UpdateTodoDto todoDto)
    {
        _logger.LogDebug($"UpdateTodo has been called with with id: {id} and todoDto: {todoDto}.");

        var todo = _mapper.Map<Todo>(todoDto) with { Id = id };

        var success = await _todosRepository.UpdateTodoAsync(todo);

        if (!success)
        {
            _logger.LogDebug($"Todo not found with id: {id}.");
        }

        return success;
    }

    public async Task<bool> DeleteTodoAsync(Guid id)
    {
        _logger.LogDebug($"DeleteTodo has been called with with id: {id}.");

        var success = await _todosRepository.DeleteTodoAsync(id);

        if (!success)
        {
            _logger.LogDebug($"Todo not found with id: {id}.");
        }

        return success;
    }
}
