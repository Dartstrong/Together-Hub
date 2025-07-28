namespace Domain.Abstractions;

public interface Entity<T> : IEntity<T>
{
    public required T Id { get; set; }
}
 