namespace BlogEngine.Domain.Commands.BlogComment.Create;

public sealed class CreateBlogCommentCommandHandler : IRequestHandler<CreateBlogCommentCommand, Guid>
{
    private readonly IBasePersistanceRepository<Entities.BlogComment> _repo;
    private readonly IReadOnlyBaseRepository<Entities.BlogPost> _blogPostRepo;
    private readonly IMapper _mapper;

    public CreateBlogCommentCommandHandler(IBasePersistanceRepository<Entities.BlogComment> repo,
                                           IReadOnlyBaseRepository<Entities.BlogPost> blogPostRepo,
                                           IMapper mapper)
    {
        _repo = repo;
        _blogPostRepo = blogPostRepo;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateBlogCommentCommand request, CancellationToken cancellationToken)
    {
        var blogPost = await _blogPostRepo.GetUniqueAsync(request.BlogPostId, cancellationToken);

        if (!blogPost.Status.HasFlag(EPublishStatus.Published))
        {
            throw new InvalidOperationException("Cannot add comment to an unpublished blog post");
        }

        var blogComment = _mapper.Map<Entities.BlogComment>(request) with
        {
            PublishDateTime = DateTime.UtcNow
        };

        await _repo.AddAsync(blogComment, cancellationToken);
        return blogComment.Id;
    }
}
