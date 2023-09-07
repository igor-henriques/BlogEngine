namespace BlogEngine.Domain.Commands.User.PatchPassword;

public sealed record PatchPasswordCommand : IRequest<Unit>
{
    public Guid UserId { get; init; }
    public string Password { get; init; }    
}
