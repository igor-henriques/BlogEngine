namespace BlogEngine.Domain.Exceptions;

/// <summary>
/// Thrown when any invalid update is made over an entity
/// </summary>
public sealed class InvalidEntityUpdateException : Exception
{
    public InvalidEntityUpdateException(string message) : base(message) { }
}
