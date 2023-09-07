namespace BlogEngine.Web.Core.ViewModels;

public sealed record BlogPostViewModel
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Content { get; init; }
    public DateTime PublishDate { get; init; }
    public DateTime LastUpdateDateTime { get; init; }
    public EPublishStatus Status { get; init; }
    public UserViewModel Author { get; init; }
    public ICollection<BlogCommentForBlogPostViewModel> Comments { get; init; } = new List<BlogCommentForBlogPostViewModel>();
    public ICollection<BlogEditorCommentForBlogPostViewModel> EditorComments { get; init; } = new List<BlogEditorCommentForBlogPostViewModel>();
}
