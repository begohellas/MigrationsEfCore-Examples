namespace MigrationsFromCode.Entities;
public class Todo
{
    public int Id { get; init; }
    public string Description { get; init; } = default!;
    public bool IsDone { get; init; }
}
