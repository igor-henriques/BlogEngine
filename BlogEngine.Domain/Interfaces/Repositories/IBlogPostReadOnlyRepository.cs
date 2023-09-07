namespace BlogEngine.Domain.Interfaces.Repositories;

public interface IBlogPostReadOnlyRepository : IReadOnlyBaseRepository<BlogPost>
{
    Task<EntityQueryResultPaginated<BlogPost>> GetPublishedPostsPaginatedAsync(
        BasePaginatedQuery paginatedQuery,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<BlogPost>> GetByAuthorAsync(
        Guid authorId, 
        CancellationToken cancellationToken = default);

    Task<IEnumerable<BlogPost>> GetByStatusAsync(
        EPublishStatus publishStatus, 
        CancellationToken cancellationToken = default);
}
