namespace BlogEngine.Domain.Entities;

public sealed record BlogEditorComment : Entity<BlogEditorComment>
{
    public string Content { get; init; }
    public Guid BlogPostId { get; init; }
    public BlogPost BlogPost { get; init; }
    public Guid EditorId { get; init; }
    public User Editor { get; init; }
    public DateTime PublishDateTime { get; init; }
}
