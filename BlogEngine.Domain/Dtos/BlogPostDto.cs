namespace BlogEngine.Domain.Dtos;

public readonly record struct BlogPostDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Content { get; init; }
    public DateTime PublishDate { get; init; }
    public DateTime LastUpdateDateTime { get; init; }
    public EPublishStatus Status { get; init; }
    public UserDto Author { get; init; }
    public ICollection<BlogCommentForBlogPostDto> Comments { get; init; }
    public ICollection<BlogEditorCommentForBlogPostDto> EditorComments { get; init; }
}
