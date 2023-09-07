namespace BlogEngine.Domain.Entities;

public sealed record BlogPost : Entity<BlogPost>
{
    public BlogPost(string title,
                    string content,
                    DateTime? publishDate,
                    DateTime? lastUpdateDateTime,
                    EPublishStatus status,
                    Guid userId,
                    User user)
    {
        Title = title;
        Content = content;
        PublishDate = publishDate;
        LastUpdateDateTime = lastUpdateDateTime;
        Status = status;
        AuthorId = userId;
        Author = user;
    }

    public BlogPost() { }

    public BlogPost(string title,
                    string content,
                    EPublishStatus status)
    {
        Title = title;
        Content = content;
        Status = status;
    }

    public string Title { get; init; }
    public string Content { get; init; }
    public DateTime? PublishDate { get; init; }
    public DateTime? LastUpdateDateTime { get; init; }
    public EPublishStatus Status { get; init; }
    public Guid AuthorId { get; init; }
    public User Author { get; init; }
    public ICollection<BlogComment> Comments { get; init; } = new List<BlogComment>();
    public ICollection<BlogEditorComment> EditorComments { get; init; } = new List<BlogEditorComment>();

    public bool IsPublishedOrSubmmited => Status.HasFlag(EPublishStatus.Published) || Status.HasFlag(EPublishStatus.Submmited);
}
