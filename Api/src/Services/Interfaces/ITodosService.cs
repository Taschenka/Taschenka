using Taschenka.Dtos;

namespace Taschenka.Services.Interfaces;

public interface ITodosService
{
    Task<IEnumerable<GetTodoDto>> GetAllTodosAsync();
    Task<GetTodoDto?> GetTodoByIdAsync(Guid id);
    Task<GetTodoDto> CreateTodoAsync(CreateTodoDto todoDto);
    Task<bool> UpdateTodoAsync(Guid id, UpdateTodoDto todoDto);
    Task<bool> DeleteTodoAsync(Guid id);
}
