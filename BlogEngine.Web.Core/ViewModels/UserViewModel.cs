namespace BlogEngine.Web.Core.ViewModels;

public sealed record UserViewModel
{
    public UserViewModel(Guid id, string email, EUserRole role)
    {
        Id = id;
        Email = email;
        Role = role;
    }

    public UserViewModel() { }

    public string Username { get; init; }
    public Guid Id { get; init; }
    public string Email { get; init; }
    public EUserRole Role { get; init; }
}
