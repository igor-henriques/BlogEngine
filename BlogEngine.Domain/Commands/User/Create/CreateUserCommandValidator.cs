namespace BlogEngine.Domain.Commands.User.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Username)} is required")
            .MaximumLength(Constants.FieldsDefinitions.MaxLengthName)
            .WithMessage(x => $"{nameof(x.Username)} maximum length is {Constants.FieldsDefinitions.MaxLengthName} characters");

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
