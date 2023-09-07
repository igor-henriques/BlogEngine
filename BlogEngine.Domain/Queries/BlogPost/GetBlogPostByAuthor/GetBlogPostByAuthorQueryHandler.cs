namespace BlogEngine.Domain.Queries.BlogPost.GetBlogPostByAuthor;

public sealed class GetBlogPostByAuthorQueryHandler : IRequestHandler<GetBlogPostByAuthorQuery, IEnumerable<BlogPostDto>>
{
    private readonly IBlogPostReadOnlyRepository _repo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public GetBlogPostByAuthorQueryHandler(IBlogPostReadOnlyRepository repo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _repo = repo;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<BlogPostDto>> Handle(GetBlogPostByAuthorQuery request, CancellationToken cancellationToken)
    {
        var writerId = Guid.Parse(_httpContextAccessor.HttpContext.Items["UserId"].ToString());
        var result = await _repo.GetByAuthorAsync(writerId, cancellationToken);
        return result.Select(_mapper.Map<BlogPostDto>);
    }
}
