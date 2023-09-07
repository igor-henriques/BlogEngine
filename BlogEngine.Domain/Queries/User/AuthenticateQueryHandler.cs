namespace BlogEngine.Domain.Queries.User;

public sealed class AuthenticateQueryHandler : IRequestHandler<AuthenticateQuery, JwtToken>
{
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IReadOnlyBaseRepository<Entities.User> _repo;

    public AuthenticateQueryHandler(IPasswordHashingService passwordHashingService,
                                    ITokenGeneratorService tokenGeneratorService,
                                    IReadOnlyBaseRepository<Entities.User> readOnlyRepo)
    {
        _passwordHashingService = passwordHashingService;
        _tokenGeneratorService = tokenGeneratorService;
        _repo = readOnlyRepo;
    }

    public async Task<JwtToken> Handle(AuthenticateQuery request, CancellationToken cancellationToken)
    {
        var user = await _repo.GetUniqueByAsync(x => x.Email == request.Email, cancellationToken);

        var isPasswordMatch = _passwordHashingService.VerifyPassword(user.HashedPassword, request.Password);
        if (!isPasswordMatch)
        {
            throw new InvalidCredentialsException($"Authentication failed for user {request.Email}");
        }

        var claims = _tokenGeneratorService.GenerateClaim(user);
        var token = _tokenGeneratorService.GenerateToken(claims);

        return token;
    }
}
