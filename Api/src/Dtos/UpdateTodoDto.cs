using System.ComponentModel.DataAnnotations;

namespace Taschenka.Dtos;

public record UpdateTodoDto
{
    [Required]
    public string name { get; init; } = null!;

    [Required]
    public string description { get; init; } = null!;

    [Required]
    public DateTime deadline { get; init; }

    [Required]
    public bool isDone { get; init; }
}
