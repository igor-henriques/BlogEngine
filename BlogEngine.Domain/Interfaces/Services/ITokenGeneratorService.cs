namespace BlogEngine.Domain.Interfaces.Services;

/// <summary>
/// Provides an authentication token generator service.
/// </summary>
public interface ITokenGeneratorService
{
    /// <summary>
    /// Generates a new authentication token.
    /// </summary>
    /// <param name="claim"></param>
    /// <returns></returns>
    JwtToken GenerateToken(List<Claim> claims = null);

    /// <summary>
    /// Generate <see cref="Claim"/> from <see cref="Entities.User"/>
    /// </summary>
    /// <param name="userRole"></param>
    /// <returns></returns>
    List<Claim> GenerateClaim(Entities.User userRole);
}
