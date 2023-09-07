namespace BlogEngine.Domain.Entities;

public sealed record BlogComment : Entity<BlogComment>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Content { get; init; }
    public Guid BlogPostId { get; init; }
    public BlogPost BlogPost { get; init; }    
    public DateTime PublishDateTime { get; init; }
}
