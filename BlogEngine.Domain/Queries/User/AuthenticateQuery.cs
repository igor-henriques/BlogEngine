namespace BlogEngine.Domain.Queries.User;

public sealed record AuthenticateQuery : IRequest<JwtToken>
{
    public string Email { get; init; }
    public string Password { get; set; }
}
