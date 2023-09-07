namespace BlogEngine.Domain.Entities.Base;

public abstract record Entity<T> where T : Entity<T>
{
    public Guid Id { get; init; }
}
