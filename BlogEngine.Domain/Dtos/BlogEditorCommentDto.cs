namespace BlogEngine.Domain.Dtos;

public readonly record struct BlogEditorCommentDto
{
    public string Content { get; init; }
    public BlogPostDto BlogPost { get; init; }
    public UserDto Editor { get; init; }
}
