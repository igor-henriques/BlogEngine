namespace BlogEngine.Domain.Queries.BlogPost.GetBlogPostByStatus;

public sealed class GetBlogPostByStatusQueryHandler : IRequestHandler<GetBlogPostByStatusQuery, IEnumerable<BlogPostDto>>
{
    private readonly IBlogPostReadOnlyRepository _repo;
    private readonly IMapper _mapper;

    public GetBlogPostByStatusQueryHandler(IBlogPostReadOnlyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BlogPostDto>> Handle(GetBlogPostByStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo.GetByStatusAsync(request.PublishStatus, cancellationToken);
        return result.Select(_mapper.Map<BlogPostDto>);
    }
}
