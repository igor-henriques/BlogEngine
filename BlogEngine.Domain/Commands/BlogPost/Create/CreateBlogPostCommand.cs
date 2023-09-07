namespace BlogEngine.Domain.Commands.BlogPost.Create;

public sealed record CreateBlogPostCommand : IRequest<Guid>
{
    public string Title { get; init; }
    public string Content { get; init; }
}
