namespace BlogEngine.Domain.Commands.BlogPost.Create;

public sealed class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, Guid>
{
    private readonly IBasePersistanceRepository<Entities.BlogPost> _repo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public CreateBlogPostCommandHandler(IBasePersistanceRepository<Entities.BlogPost> repo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _repo = repo;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var authorId = Guid.Parse(_httpContextAccessor.HttpContext.Items["UserId"].ToString());

        var blogPost = _mapper.Map<Entities.BlogPost>(request) with
        {            
            LastUpdateDateTime = DateTime.UtcNow,
            Status = EPublishStatus.Submmited,
            AuthorId = authorId
        };

        await _repo.AddAsync(blogPost, cancellationToken);

        return blogPost.Id;
    }
}
