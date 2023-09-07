namespace BlogEngine.Domain.Commands.BlogEditorComment.Create;

public sealed class CreateBlogEditorCommentCommandValidator : AbstractValidator<CreateBlogEditorCommentCommand>
{
    public CreateBlogEditorCommentCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Content)} is required.");

        RuleFor(x => x.BlogPostId)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.BlogPostId)} is required.");
    }
}
