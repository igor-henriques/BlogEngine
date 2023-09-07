namespace BlogEngine.Domain.Commands.User.PatchPassword;

public sealed class PatchPasswordCommandValidator : AbstractValidator<PatchPasswordCommand>
{
    public PatchPasswordCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(x => Guid.Empty)
            .WithMessage(x => $"{nameof(x.UserId)} is required");            

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Password)} is required")
            .Must(Helper.CheckForPasswordRequiredCharacters)
            .WithMessage(x => $"{nameof(x.Password)} must have at least one uppercase letter, one lowercase letter, one number and one special character")
            .MaximumLength(Constants.FieldsDefinitions.MaxLengthHashedPassword / 2)
            .WithMessage(x => $"{nameof(x.Password)} maximum length is {Constants.FieldsDefinitions.MaxLengthHashedPassword / 2} characters");
    }
}
