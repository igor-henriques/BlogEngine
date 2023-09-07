namespace BlogEngine.Domain.Commands.User.PatchRole;

public sealed record PatchRoleCommand : IRequest<Unit>
{
    public Guid UserId { get; init; }    
    public Core.Enums.EUserRole Role { get; init; }
}
