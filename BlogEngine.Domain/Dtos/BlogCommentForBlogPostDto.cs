namespace BlogEngine.Domain.Dtos;

public readonly record struct BlogCommentForBlogPostDto
{
    public string Username { get; init; }
    public string Content { get; init; }
    public DateTime PublishDateTime { get; init; }
}
