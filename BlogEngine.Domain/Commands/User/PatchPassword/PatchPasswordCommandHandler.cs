namespace BlogEngine.Domain.Commands.User.PatchPassword;

public sealed class PatchPasswordCommandHandler : IRequestHandler<PatchPasswordCommand, Unit>
{
    private readonly IBasePersistanceRepository<Entities.User> _repo;
    private readonly IPasswordHashingService _passwordHashingService;

    public PatchPasswordCommandHandler(
        IBasePersistanceRepository<Entities.User> repo,
        IPasswordHashingService passwordHashingService)
    {
        _repo = repo;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<Unit> Handle(PatchPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _repo.GetUniqueAsync(request.UserId, cancellationToken);
        var hashedPassword = _passwordHashingService.HashPassword(request.Password);

        var updatedUser = user with
        {
            HashedPassword = hashedPassword
        };

        await _repo.UpdateAsync(updatedUser, cancellationToken);

        return Unit.Value;
    }
}
