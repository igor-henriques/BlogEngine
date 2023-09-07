namespace BlogEngine.Domain.Queries.User;

public sealed class AuthenticateQueryValidator : AbstractValidator<AuthenticateQuery>
{
    public AuthenticateQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Email)} is required")
            .MaximumLength(Constants.FieldsDefinitions.MaxLengthEmail)
            .WithMessage(x => $"{nameof(x.Email)} maximum length is {Constants.FieldsDefinitions.MaxLengthEmail} characters");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Password)} is required")
            .Must(Helper.CheckForPasswordRequiredCharacters)
            .WithMessage(x => $"{nameof(x.Password)} must have at least one uppercase letter, one lowercase letter, one number and one special character")
            .MaximumLength(Constants.FieldsDefinitions.MaxLengthHashedPassword / 2)
            .WithMessage(x => $"{nameof(x.Password)} maximum length is {Constants.FieldsDefinitions.MaxLengthHashedPassword / 2} characters");
    }
}
