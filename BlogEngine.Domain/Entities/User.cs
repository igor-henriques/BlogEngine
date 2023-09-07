namespace BlogEngine.Domain.Entities;

public sealed record User : Entity<User>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string HashedPassword { get; init; }    
    public EUserRole Role { get; init; }

    public User() { }

    public User(string username,
                string email,
                string hashedPassword,
                EUserRole role)
    {
        Username = username;
        Email = email;
        HashedPassword = hashedPassword;    
        Role = role;
    }

    public bool IsWriterOrEditor()
    {
        return Role.HasFlag(EUserRole.Writer) || Role.HasFlag(EUserRole.Editor);
    }
}
