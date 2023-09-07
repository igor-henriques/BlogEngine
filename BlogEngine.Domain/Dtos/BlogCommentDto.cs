namespace BlogEngine.Domain.Dtos;

public readonly record struct BlogCommentDto
{
    public string Username { get; init; }
    public string Email { get; init; }
    public BlogPostDto BlogPost { get; init; }
    public string Content { get; init; }
}
