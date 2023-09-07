namespace BlogEngine.Web.Core.ViewModels;

public sealed record BlogCommentForBlogPostViewModel
{
    public string Username { get; init; }
    public string Content { get; init; }
    public DateTime PublishDateTime { get; init; }
}
