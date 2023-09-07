namespace BlogEngine.Domain.Commands.BlogPost.Reprove;

public sealed class ReproveBlogPostCommandValidator : AbstractValidator<ReproveBlogPostCommand>
{
    public ReproveBlogPostCommandValidator()
    {
        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Reason)} is required.");

        RuleFor(x => x.BlogPostId)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.BlogPostId)} is required.");
    }
}
