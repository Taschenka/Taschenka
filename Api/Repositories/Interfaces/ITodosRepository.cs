using Taschenka.Entities;

namespace Taschenka.Repositories.Interfaces;

public interface ITodosRepository
{
    Task<IEnumerable<Todo>> GetAllTodosAsync();
    Task<Todo?> GetTodoByIdAsync(Guid id);
    Task CreateTodoAsync(Todo todo);
    Task<bool> UpdateTodoAsync(Todo todo);
    Task<bool> DeleteTodoAsync(Guid id);
}
