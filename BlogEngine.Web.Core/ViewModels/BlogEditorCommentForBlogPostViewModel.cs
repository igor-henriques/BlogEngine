namespace BlogEngine.Web.Core.ViewModels;

public sealed record BlogEditorCommentForBlogPostViewModel
{
    public string Content { get; init; }
    public UserViewModel Editor { get; init; }
}
