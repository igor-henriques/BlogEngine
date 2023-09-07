namespace BlogEngine.Domain.Commands.BlogPost.Put;

public sealed class PutBlogPostCommand : IRequest<Unit>
{
    public Guid BlogPostId { get; init; }
    public string Title { get; init; }
    public string Content { get; init; }
    public bool SendToReview { get; init; }
}
