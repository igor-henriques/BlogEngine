namespace BlogEngine.Domain.Commands.BlogPost.Reprove;

public sealed class ReproveBlogPostCommand : IRequest<Unit>
{
    public Guid BlogPostId { get; init; }
    public string Reason { get; init; }    
}
