namespace BlogEngine.Domain.Queries.BlogPost.GetBlogPostByStatus;

public sealed record GetBlogPostByStatusQuery : IQuery<IEnumerable<BlogPostDto>>
{
    public GetBlogPostByStatusQuery(EPublishStatus publishStatus)
    {
        PublishStatus = publishStatus;
    }

    public EPublishStatus PublishStatus { get; init; }
}
