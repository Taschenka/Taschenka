using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using Taschenka.Entities;
using Taschenka.Repositories.Interfaces;

namespace Taschenka.Repositories;

public class MongoDbTodosRepository : ITodosRepository
{
    private const string databaseName = "taschenka";
    private const string collectionName = "todos";
    private readonly IMongoCollection<Todo> _todosCollection;

    public MongoDbTodosRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(databaseName);
        _todosCollection = database.GetCollection<Todo>(collectionName);
    }

    public async Task<IEnumerable<Todo>> GetAllTodosAsync()
    {
        return await _todosCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Todo?> GetTodoByIdAsync(Guid id)
    {
        var filter = Builders<Todo>.Filter.Eq(todo => todo.Id, id);

        return await _todosCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task CreateTodoAsync(Todo todo)
    {
        await _todosCollection.InsertOneAsync(todo);
    }

    public async Task<bool> UpdateTodoAsync(Todo todo)
    {
        var filter = Builders<Todo>.Filter.Eq(existingTodo => existingTodo.Id, todo.Id);

        var result = await _todosCollection.ReplaceOneAsync(filter, todo);

        if (result.MatchedCount == 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteTodoAsync(Guid id)
    {
        var filter = Builders<Todo>.Filter.Eq(todo => todo.Id, id);

        var result = await _todosCollection.DeleteOneAsync(filter);

        if (result.DeletedCount == 0)
        {
            return false;
        }

        return true;
    }
}
