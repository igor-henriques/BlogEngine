namespace BlogEngine.Domain.Commands.BlogPost.Create;

public sealed class CreateBlogPostCommandValidator : AbstractValidator<CreateBlogPostCommand>
{
    public CreateBlogPostCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Title)} is required.")
            .MaximumLength(Constants.FieldsDefinitions.MaxLengthPostTitle)
            .WithMessage(x => $"{nameof(x.Title)} has a {Constants.FieldsDefinitions.MaxLengthPostTitle} maximum caracters");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Content)} is required.");
    }
}
