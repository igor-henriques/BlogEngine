namespace BlogEngine.Domain.Queries.BlogPost.GetBlogPostByAuthor;

public sealed record GetBlogPostByAuthorQuery : IQuery<IEnumerable<BlogPostDto>>
{
}
