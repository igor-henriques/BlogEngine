namespace BlogEngine.Domain.Commands.BlogPost.Put;

public sealed class PutBlogPostCommandValidator : AbstractValidator<PutBlogPostCommand>
{
    public PutBlogPostCommandValidator()
    {
        RuleFor(x => x.BlogPostId)
          .NotEmpty().WithMessage(x => $"{nameof(x.BlogPostId)} is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(x => $"{nameof(x.Title)} is required.")            
            .MaximumLength(Constants.FieldsDefinitions.MaxLengthPostTitle)
            .WithMessage(x => $"{nameof(x.Title)} has a {Constants.FieldsDefinitions.MaxLengthPostTitle} maximum caracters");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Content)} is required");
    }
}
