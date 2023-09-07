namespace BlogEngine.Web.Core.ViewModels;

public sealed record LoginViewModel
{
    [Required]
    [MaxLength(Constants.FieldsDefinitions.MaxLengthEmail)]
    public string Email { get; set; }

    [Required]
    [MaxLength(Constants.FieldsDefinitions.MaxLengthHashedPassword)]
    [MinLength(8)]
    [PasswordPropertyText]
    public string Password { get; set; }
}
