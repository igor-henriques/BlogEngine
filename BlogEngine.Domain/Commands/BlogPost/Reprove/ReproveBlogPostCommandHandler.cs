namespace BlogEngine.Domain.Commands.BlogPost.Reprove;

public sealed class ReproveBlogPostCommandHandler : IRequestHandler<ReproveBlogPostCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IReadOnlyBaseRepository<Entities.User> _userRepo;
    private readonly IBasePersistanceRepository<Entities.BlogPost> _blogPostRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ReproveBlogPostCommandHandler(IMediator mediator,
                                         IReadOnlyBaseRepository<Entities.User> userRepo,
                                         IBasePersistanceRepository<Entities.BlogPost> blogPostRepo,
                                         IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _userRepo = userRepo;
        _blogPostRepo = blogPostRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(ReproveBlogPostCommand request, CancellationToken cancellationToken)
    {
        var editorId = Guid.Parse(_httpContextAccessor.HttpContext.Items["UserId"].ToString());

        var editor = await _userRepo.GetUniqueAsync(editorId, cancellationToken);

        if (!editor.Role.HasFlag(EUserRole.Editor))
        {
            throw new UnauthorizedAccessException($"User {editor.Email} is not an editor");
        }

        var blogPost = await _blogPostRepo.GetUniqueAsync(request.BlogPostId, cancellationToken);
        var repprovedBlogPost = blogPost with
        {
            Status = blogPost.Status | EPublishStatus.Reproved,
            LastUpdateDateTime = DateTime.UtcNow
        };

        _ = await _mediator.Send(new CreateBlogEditorCommentCommand(request.Reason, request.BlogPostId), 
                cancellationToken);

        await _blogPostRepo.UpdateAsync(repprovedBlogPost, cancellationToken);

        return Unit.Value;
    }
}
