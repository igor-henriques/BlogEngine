namespace BlogEngine.Infrastructure.Repository.Repositories.ReadOnly;

internal class ReadOnlyBaseRepository<TEntity> : IReadOnlyBaseRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    private readonly BlogDbContext _context;
    private readonly ILogger<ReadOnlyBaseRepository<TEntity>> _logger;

    public ReadOnlyBaseRepository(BlogDbContext context, ILogger<ReadOnlyBaseRepository<TEntity>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public virtual async Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null || entity == default)
        {
            return false;
        }

        var response = await _context
            .Set<TEntity>()
            .AsNoTracking()
            .AnyAsync(x => x.Id == entity.Id, cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
            nameof(ExistsAsync),
            nameof(ReadOnlyBaseRepository<TEntity>),
            response);


        return response;
    }

    public virtual async Task<bool> ExistsByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var response = await _context
            .Set<TEntity>()
            .AsNoTracking()
            .AnyAsync(predicate, cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
            nameof(ExistsByAsync),
            nameof(ReadOnlyBaseRepository<TEntity>),
            response);

        return response;
    }

    public virtual async Task<TEntity> GetUniqueAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == default)
        {
            _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
                nameof(GetUniqueAsync),
                nameof(ReadOnlyBaseRepository<TEntity>),
                "null");

            return null;
        }

        var response = await _context
            .Set<TEntity>()
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
            nameof(GetUniqueAsync),
            nameof(ReadOnlyBaseRepository<TEntity>),
            response);

        return response;
    }

    public virtual async Task<TEntity> GetUniqueByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var response = await _context
            .Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .FirstOrDefaultAsync(cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
            nameof(GetUniqueByAsync),
            nameof(ReadOnlyBaseRepository<TEntity>),
            response);

        return response;
    }

    public virtual async Task<EntityQueryResultPaginated<TEntity>> GetByPaginatedAsync(
        Expression<Func<TEntity, bool>> predicate,
        BasePaginatedQuery basePaginated,
        CancellationToken cancellationToken = default)
    {
        var response = await _context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .Skip((basePaginated.PageNumber - 1) * basePaginated.ItemsPerPage)
            .Take(basePaginated.ItemsPerPage)
            .ToListAsync(cancellationToken);

        var count = await CountAsync(cancellationToken);

        var paginatedResult = new EntityQueryResultPaginated<TEntity>()
        {
            Data = response,
            PageNumber = basePaginated.PageNumber,
            ItemsPerPage = basePaginated.ItemsPerPage > response.Count ? response.Count : basePaginated.ItemsPerPage,
            TotalCount = count,
            TotalPages = (int)Math.Ceiling(count / (double)basePaginated.ItemsPerPage)
        };

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
           nameof(GetByPaginatedAsync),
           nameof(ReadOnlyBaseRepository<TEntity>),
           paginatedResult);

        return paginatedResult;
    }

    public virtual async Task<EntityQueryResultPaginated<TEntity>> GetPaginatedAsync(
        BasePaginatedQuery paginatedQuery,
        CancellationToken cancellationToken = default)
    {
        var response = await _context
            .Set<TEntity>()
            .AsNoTracking()
            .Skip((paginatedQuery.PageNumber - 1) * paginatedQuery.ItemsPerPage)
            .Take(paginatedQuery.ItemsPerPage)
            .ToListAsync(cancellationToken);

        var count = await CountAsync(cancellationToken);

        var paginatedResult = new EntityQueryResultPaginated<TEntity>()
        {
            Data = response,
            PageNumber = paginatedQuery.PageNumber,
            ItemsPerPage = paginatedQuery.ItemsPerPage > response.Count ? response.Count : paginatedQuery.ItemsPerPage,
            TotalCount = count,
            TotalPages = (int)Math.Ceiling(count / (double)paginatedQuery.ItemsPerPage)
        };

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
           nameof(GetPaginatedAsync),
           nameof(ReadOnlyBaseRepository<TEntity>),
           paginatedResult);

        return paginatedResult;
    }

    public virtual async Task<int> CountByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var response = await _context
            .Set<TEntity>()
            .AsNoTracking()
            .CountAsync(predicate, cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
           nameof(CountByAsync),
           nameof(ReadOnlyBaseRepository<TEntity>),
           response);

        return response;
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        var response = await _context
            .Set<TEntity>()
            .AsNoTracking()
            .CountAsync(cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
           nameof(CountAsync),
           nameof(ReadOnlyBaseRepository<TEntity>),
           response);

        return response;
    }
}
