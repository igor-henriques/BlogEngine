namespace BlogEngine.Infrastructure.Service.Services;

internal sealed class TokenGeneratorService : ITokenGeneratorService
{
    private readonly IOptions<JwtAuthenticationOptions> _jwtOptions;

    public TokenGeneratorService(IOptions<JwtAuthenticationOptions> jwtOptions)
    {
        ArgumentValidator.ThrowIfNullOrDefault(jwtOptions.Value, nameof(jwtOptions));
        _jwtOptions = jwtOptions;
    }

    public JwtToken GenerateToken(List<Claim> claims = null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Value.Key);
        var expiresAt = DateTime.UtcNow.AddHours(_jwtOptions.Value.TokenHoursDuration);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = expiresAt,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        if (claims != null)
        {
            tokenDescriptor.Subject = new ClaimsIdentity(claims);
        }

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var jwt = tokenHandler.WriteToken(token);

        return new JwtToken
        {
            Token = jwt,
            ExpiresAt = expiresAt
        };
    }

    public List<Claim> GenerateClaim(User userRole)
    {
        return new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userRole.Id.ToString()),
            new Claim(ClaimTypes.Role, userRole.Role.ToString())
        };
    }
}
