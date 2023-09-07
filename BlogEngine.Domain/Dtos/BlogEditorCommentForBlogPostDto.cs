namespace BlogEngine.Domain.Dtos;

public readonly record struct BlogEditorCommentForBlogPostDto
{
    public string Content { get; init; }
    public UserDto Editor { get; init; }
    public DateTime PublishDateTime { get; init; }
}
