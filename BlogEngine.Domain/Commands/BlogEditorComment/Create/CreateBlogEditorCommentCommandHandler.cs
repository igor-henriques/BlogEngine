namespace BlogEngine.Domain.Commands.BlogEditorComment.Create;

public sealed class CreateBlogEditorCommentCommandHandler : IRequestHandler<CreateBlogEditorCommentCommand, Guid>
{
    private readonly IBasePersistanceRepository<Entities.BlogEditorComment> _repo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public CreateBlogEditorCommentCommandHandler(IBasePersistanceRepository<Entities.BlogEditorComment> repo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _repo = repo;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> Handle(CreateBlogEditorCommentCommand request, CancellationToken cancellationToken)
    {
        var blogEditorComment = _mapper.Map<Entities.BlogEditorComment>(request) with
        {
            EditorId = Guid.Parse(_httpContextAccessor.HttpContext.Items["UserId"].ToString())
        };

        await _repo.AddAsync(blogEditorComment, cancellationToken);
        return blogEditorComment.Id;
    }
}
