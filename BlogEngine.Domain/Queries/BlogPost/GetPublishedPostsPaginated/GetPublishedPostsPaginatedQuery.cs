namespace BlogEngine.Domain.Queries.BlogPost.GetPublishedPostsPaginated;

public sealed record GetPublishedPostsPaginatedQuery : BasePaginatedQuery, IQuery<EntityQueryResultPaginated<BlogPostDto>>
{
    public GetPublishedPostsPaginatedQuery(
        int pageNumber,
        int itemsPerPage) : base(pageNumber, itemsPerPage)
    {

    }
}
