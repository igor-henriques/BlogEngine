namespace BlogEngine.Domain.Interfaces.Repositories.Base;

public interface IBasePersistanceRepository<TEntity> where TEntity : Entity<TEntity>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> GetUniqueAsync(Guid id, CancellationToken cancellationToken = default);
}
