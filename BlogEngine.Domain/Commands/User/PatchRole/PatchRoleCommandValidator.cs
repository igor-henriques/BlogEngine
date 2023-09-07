namespace BlogEngine.Domain.Commands.User.PatchRole;

public sealed class PatchRoleCommandValidator : AbstractValidator<PatchRoleCommand>
{
    public PatchRoleCommandValidator()
    {
        RuleFor(x => x.UserId)
           .NotEqual(x => Guid.Empty)
           .WithMessage(x => $"{nameof(x.UserId)} is required");
    }
}
