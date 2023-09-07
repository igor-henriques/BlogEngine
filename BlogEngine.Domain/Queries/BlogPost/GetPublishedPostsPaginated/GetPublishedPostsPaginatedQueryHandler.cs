namespace BlogEngine.Domain.Queries.BlogPost.GetPublishedPostsPaginated;

public sealed class GetPublishedPostsPaginatedQueryHandler : IRequestHandler<GetPublishedPostsPaginatedQuery, EntityQueryResultPaginated<BlogPostDto>>
{
    private readonly IBlogPostReadOnlyRepository _repo;
    private readonly IMapper _mapper;

    public GetPublishedPostsPaginatedQueryHandler(
        IBlogPostReadOnlyRepository repo,
        IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<EntityQueryResultPaginated<BlogPostDto>> Handle(GetPublishedPostsPaginatedQuery request, CancellationToken cancellationToken)
    {       
        var paginatedResult = await _repo.GetPublishedPostsPaginatedAsync(request, cancellationToken);
        return MapToDto(paginatedResult);
    }

    private EntityQueryResultPaginated<BlogPostDto> MapToDto(EntityQueryResultPaginated<Entities.BlogPost> paginatedResult)
    {
        return paginatedResult.MapToDto(paginatedResult.Data.Select(_mapper.Map<BlogPostDto>));
    }
}
