namespace BlogEngine.Domain.Commands.User.Create;

public sealed record class CreateUserCommand : IRequest<Guid>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }   
    public Core.Enums.EUserRole? Role { get; init; }
}
