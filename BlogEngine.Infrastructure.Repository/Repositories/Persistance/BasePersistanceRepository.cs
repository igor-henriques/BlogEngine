namespace BlogEngine.Infrastructure.Repository.Repositories.Persistance;

internal class BasePersistanceRepository<TEntity> : IBasePersistanceRepository<TEntity> where TEntity : Entity<TEntity>
{
    private readonly BlogDbContext _context;
    private readonly ILogger<BasePersistanceRepository<TEntity>> _logger;

    public BasePersistanceRepository(BlogDbContext context, ILogger<BasePersistanceRepository<TEntity>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName}: {entity} was added: {value}",
           nameof(AddAsync),
           nameof(BasePersistanceRepository<TEntity>),
           nameof(TEntity),
           entity);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName}: {entity} was marked to removal, but not yet commited: {value}",
            nameof(DeleteAsync),
            nameof(BasePersistanceRepository<TEntity>),
            nameof(TEntity),
            entity);
    }

    public async Task<TEntity> GetUniqueAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _context
            .Set<TEntity>()
            .FindAsync(new object[] { id }, cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
            nameof(GetUniqueAsync),
            nameof(BasePersistanceRepository<TEntity>),
            response);

        return response;
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var originalEntity = await _context.Set<TEntity>()
                .FindAsync(new object[] { entity.Id }, cancellationToken);

        _context.Entry(originalEntity).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName}: {entity} was updated: {value}",
            nameof(UpdateAsync),
            nameof(BasePersistanceRepository<TEntity>),
            nameof(TEntity),
            entity);
    }
}
