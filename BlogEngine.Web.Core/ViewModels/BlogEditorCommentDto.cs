namespace BlogEngine.Web.Core.ViewModels;

public sealed record BlogEditorCommentDto
{
    public string Content { get; init; }
    public BlogPostViewModel BlogPost { get; init; }
    public UserViewModel Editor { get; init; }
}
