namespace BlogEngine.Web.Core.ViewModels;

public sealed record RegisterViewModel
{
    [Required]
    [MaxLength(Constants.FieldsDefinitions.MaxLengthEmail)]
    public string Email { get; init; }

    [Required]
    [MaxLength(Constants.FieldsDefinitions.MaxLengthHashedPassword)]
    [MinLength(8)]
    [PasswordPropertyText]
    public string Password { get; init; }
}
