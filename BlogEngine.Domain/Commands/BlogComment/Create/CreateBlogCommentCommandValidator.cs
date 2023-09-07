namespace BlogEngine.Domain.Commands.BlogComment.Create;

public sealed class CreateBlogCommentCommandValidator : AbstractValidator<CreateBlogCommentCommand>
{
    public CreateBlogCommentCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Content)} is required.");

        RuleFor(x => x.BlogPostId)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.BlogPostId)} is required.");

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Username)} is required.")
            .MaximumLength(Constants.FieldsDefinitions.MaxLengthName)
            .WithMessage(x => $"{nameof(x.Username)} has a {Constants.FieldsDefinitions.MaxLengthName} maximum caracters");

        RuleFor(x => x.Email)
           .NotEmpty()
           .WithMessage(x => $"{nameof(x.Email)} is required.")
           .MaximumLength(Constants.FieldsDefinitions.MaxLengthEmail)
           .WithMessage(x => $"{nameof(x.Email)} has a {Constants.FieldsDefinitions.MaxLengthEmail} maximum caracters");
    }
}
