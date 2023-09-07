namespace BlogEngine.Domain.Models.Options;

public sealed record JwtAuthenticationOptions
{
    public string Key { get; init; }
    public int TokenHoursDuration { get; init; }
}
