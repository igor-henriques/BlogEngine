namespace BlogEngine.Infrastructure.Repository.Repositories.ReadOnly;

internal sealed class BlogPostReadOnlyRepository : ReadOnlyBaseRepository<BlogPost>, IBlogPostReadOnlyRepository
{
    private readonly BlogDbContext _context;
    private readonly ILogger<ReadOnlyBaseRepository<BlogPost>> _logger;

    public BlogPostReadOnlyRepository(BlogDbContext context,
                                      ILogger<ReadOnlyBaseRepository<BlogPost>> logger) : base(context, logger)
    {
        this._context = context;
        this._logger = logger;
    }

    public async Task<EntityQueryResultPaginated<BlogPost>> GetPublishedPostsPaginatedAsync(
        BasePaginatedQuery paginatedQuery,
        CancellationToken cancellationToken = default)
    {
        var query = _context.BlogPosts.AsNoTracking()                                      
                                      .Include(x => x.Author)
                                      .Include(x => x.Comments.OrderByDescending(y => y.PublishDateTime))                                      
                                      .Where(x => (x.Status & EPublishStatus.Published) == EPublishStatus.Published)
                                      .OrderByDescending(x => x.PublishDate)
                                      .Skip((paginatedQuery.PageNumber - 1) * paginatedQuery.ItemsPerPage)
                                      .Take(paginatedQuery.ItemsPerPage);

        var response = await query.ToListAsync(cancellationToken);
        var count = await base.CountAsync(cancellationToken);

        return CreatePaginatedResult(response, count, paginatedQuery);
    }

    public async Task<IEnumerable<BlogPost>> GetByAuthorAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        var result = await _context.BlogPosts.AsNoTracking()
                                      .Where(x => x.AuthorId == authorId)
                                      .Include(x => x.EditorComments)
                                      .Include(x => x.Comments)
                                      .Include(x => x.Author)
                                      .ToListAsync(cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
               nameof(GetByAuthorAsync),
               nameof(ReadOnlyBaseRepository<BlogPost>),
               result);

        return result;
    }

    public async Task<IEnumerable<BlogPost>> GetByStatusAsync(EPublishStatus publishStatus, CancellationToken cancellationToken = default)
    {
        var result = await _context.BlogPosts.AsNoTracking()
                                      .Where(x => (publishStatus & x.Status) == x.Status)
                                      .Include(x => x.Author)
                                      .Include(x => x.EditorComments) 
                                      .ThenInclude(x => x.Editor)
                                      .Include(x => x.Comments)
                                      .ToListAsync(cancellationToken);

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
               nameof(GetByStatusAsync),
               nameof(ReadOnlyBaseRepository<BlogPost>),
               result);

        return result;
    }
    private EntityQueryResultPaginated<BlogPost> CreatePaginatedResult(List<BlogPost> response, int count, BasePaginatedQuery paginatedQuery)
    {
        var paginatedResult = new EntityQueryResultPaginated<BlogPost>()
        {
            Data = response,
            PageNumber = paginatedQuery.PageNumber,
            ItemsPerPage = paginatedQuery.ItemsPerPage > response.Count ? response.Count : paginatedQuery.ItemsPerPage,
            TotalCount = count,
            TotalPages = (int)Math.Ceiling(count / (double)paginatedQuery.ItemsPerPage)
        };

        _logger.LogInformation("{methodName} from {sourceName} retrieved {response}",
               nameof(GetPublishedPostsPaginatedAsync),
               nameof(ReadOnlyBaseRepository<BlogPost>),
               paginatedResult);

        return paginatedResult;
    }    
}
