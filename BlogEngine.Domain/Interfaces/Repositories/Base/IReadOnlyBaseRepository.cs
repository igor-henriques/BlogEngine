namespace BlogEngine.Domain.Interfaces.Repositories.Base;

/// <summary>
/// All readonly repositories SHOULD NOT implement data changes methods.
/// All readonly repositories SHOULD implement AsNoTracking
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public interface IReadOnlyBaseRepository<TEntity> where TEntity : Entity<TEntity>
{
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<EntityQueryResultPaginated<TEntity>> GetByPaginatedAsync(Expression<Func<TEntity, bool>> predicate, BasePaginatedQuery basePaginated, CancellationToken cancellationToken = default);
    Task<EntityQueryResultPaginated<TEntity>> GetPaginatedAsync(BasePaginatedQuery basePaginated, CancellationToken cancellationToken = default);
    Task<TEntity> GetUniqueAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> GetUniqueByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}
