namespace BlogEngine.Domain.Commands.BlogPost.UpdateStatus;

public sealed class UpdateBlogPostStatusCommandValidator : AbstractValidator<UpdateBlogPostStatusCommand>
{
    public UpdateBlogPostStatusCommandValidator()
    {
        RuleFor(x => x.BlogPostId)
          .NotEmpty().WithMessage(x => $"{nameof(x.BlogPostId)} is required");
    }
}
