namespace BlogEngine.Domain.Commands.User.PatchRole;

public sealed class PatchRoleCommandHandler : IRequestHandler<PatchRoleCommand, Unit>
{
    private readonly IBasePersistanceRepository<Entities.User> _repo;    

    public PatchRoleCommandHandler(IBasePersistanceRepository<Entities.User> repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(PatchRoleCommand request, CancellationToken cancellationToken)
    {        
        var user = await _repo.GetUniqueAsync(request.UserId, cancellationToken) with
        {
            Role = request.Role
        };

        await _repo.UpdateAsync(user, cancellationToken);

        return Unit.Value;
    }
}
