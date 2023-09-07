namespace BlogEngine.Domain.Dtos;

public readonly record struct UserDto
{
    public UserDto(string username, Guid id, string email, EUserRole role)
    {
        Username = username;
        Id = id;
        Email = email;
        Role = role;
    }

    public UserDto() { }

    public string Username { get; init; }
    public Guid Id { get; init; }
    public string Email { get; init; }
    public EUserRole Role { get; init; }
}
