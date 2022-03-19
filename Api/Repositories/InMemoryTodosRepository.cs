using Taschenka.Entities;
using Taschenka.Repositories.Interfaces;

namespace Taschenka.Repositories;

public class InMemoryTodosRepository : ITodosRepository
{
    private List<Todo> _todos = new()
    {
        new Todo
        {
            Id = Guid.NewGuid(),
            name = "Create GitHub organization",
            description = "Check this link: https://github.com/organizations/plan and choose the free plan.",
            deadline = new DateTime(2022, 3, 8, 23, 59, 59),
            isDone = true,
        },
        new Todo
        {
            Id = Guid.NewGuid(),
            name = "Create repositories",
            description = "Create repositories for the newly created organization and add some sample code to them.",
            deadline = new DateTime(2022, 3, 8, 23, 59, 59),
            isDone = true,
        },
        new Todo
        {
            Id = Guid.NewGuid(),
            name = "Wirte the API",
            description = "Write the code for the API, with in-memory database.",
            deadline = new DateTime(2022, 3, 9, 23, 59, 59),
            isDone = false,
        },
    };

    public async Task<IEnumerable<Todo>> GetAllTodosAsync()
    {
        return await Task.FromResult(_todos);
    }

    public async Task<Todo?> GetTodoByIdAsync(Guid id)
    {
        var filter = (Todo todo) => Guid.Equals(todo.Id, id);

        var item = _todos.Where(filter).SingleOrDefault();

        return await Task.FromResult(item);
    }

    public async Task CreateTodoAsync(Todo todo)
    {
        _todos.Add(todo);

        await Task.CompletedTask;
    }

    public async Task<bool> UpdateTodoAsync(Todo todo)
    {
        Predicate<Todo> filter = existingTodo => Guid.Equals(existingTodo.Id, todo.Id);

        var index = _todos.FindIndex(filter);

        if (index == -1)
        {
            return await Task.FromResult(false);
        }

        _todos[index] = todo;

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteTodoAsync(Guid id)
    {
        Predicate<Todo> filter = todo => Guid.Equals(todo.Id, id);

        var index = _todos.FindIndex(filter);

        if (index == -1)
        {
            return await Task.FromResult(false);
        }

        _todos.RemoveAt(index);

        return await Task.FromResult(true);
    }
}
