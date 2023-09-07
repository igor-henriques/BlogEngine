namespace BlogEngine.Domain.Commands.BlogPost.UpdateStatus;

public sealed record UpdateBlogPostStatusCommand : IRequest<Unit>
{
    public Guid BlogPostId { get; init; }
    public EPublishStatus Status { get; init; }    
}
