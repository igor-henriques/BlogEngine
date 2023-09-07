namespace BlogEngine.Domain.Commands.BlogEditorComment.Create;

public sealed class CreateBlogEditorCommentCommand : IRequest<Guid>
{
    public CreateBlogEditorCommentCommand(string content, Guid blogPostId)
    {
        Content = content;
        BlogPostId = blogPostId;
    }

    public string Content { get; init; }
    public Guid BlogPostId { get; init; }
}
