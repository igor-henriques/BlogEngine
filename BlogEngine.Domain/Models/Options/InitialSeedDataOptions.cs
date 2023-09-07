namespace BlogEngine.Domain.Models.Options;

public sealed class InitialSeedDataOptions
{
    public CreateUserCommand FirstEditorUser { get; init; }
    public CreateUserCommand FirstWriterUser { get; init; }
}
