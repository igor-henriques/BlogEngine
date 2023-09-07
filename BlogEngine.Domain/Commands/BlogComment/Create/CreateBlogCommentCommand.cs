namespace BlogEngine.Domain.Commands.BlogComment.Create;

public sealed class CreateBlogCommentCommand : IRequest<Guid>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public Guid BlogPostId { get; init; }
    public string Content { get; init; }
}
