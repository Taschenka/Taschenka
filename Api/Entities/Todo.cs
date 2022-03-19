namespace Taschenka.Entities;

public record Todo
{
    public Guid Id { get; init; }
    public string name { get; init; } = null!;
    public string description { get; init; } = null!;
    public DateTime deadline { get; init; }
    public bool isDone { get; init; }
}
