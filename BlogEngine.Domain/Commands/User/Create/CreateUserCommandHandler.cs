namespace BlogEngine.Domain.Commands.User.Create;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IBasePersistanceRepository<Entities.User> _repo;    
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IBasePersistanceRepository<Entities.User> repo,
                                    IMapper mapper,
                                    IPasswordHashingService passwordHashingService)
    {
        _repo = repo;
        _mapper = mapper;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Entities.User>(request);
        var hashedPassword = _passwordHashingService.HashPassword(request.Password);
        var updatedUser = user with
        {
            HashedPassword = hashedPassword
        };
        await _repo.AddAsync(updatedUser, cancellationToken);

        return updatedUser.Id;
    }
}
