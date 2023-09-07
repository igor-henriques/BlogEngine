namespace BlogEngine.Domain.Commands.BlogPost.UpdateStatus;

public sealed class UpdateBlogPostStatusCommandHandler : IRequestHandler<UpdateBlogPostStatusCommand, Unit>
{
    private readonly IBasePersistanceRepository<Entities.BlogPost> _repo;

    public UpdateBlogPostStatusCommandHandler(IBasePersistanceRepository<Entities.BlogPost> repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(UpdateBlogPostStatusCommand request, CancellationToken cancellationToken)
    {
        var blogPost = await _repo.GetUniqueAsync(request.BlogPostId, cancellationToken);
        var newStatus = blogPost.Status;

        if (request.Status.HasFlag(EPublishStatus.Approved))
        {
            newStatus &= ~EPublishStatus.Reproved;
            newStatus |= EPublishStatus.Approved;
            blogPost = blogPost with
            {
                LastUpdateDateTime = DateTime.UtcNow
            };
        }
        else if (request.Status.HasFlag(EPublishStatus.Reproved))
        {
            newStatus &= ~EPublishStatus.Approved;
            newStatus &= ~EPublishStatus.Published;
            newStatus |= EPublishStatus.Reproved;
            blogPost = blogPost with
            {
                LastUpdateDateTime = DateTime.UtcNow
            };
        }
        else if (!request.Status.HasFlag(EPublishStatus.Reproved) && request.Status.HasFlag(EPublishStatus.Published))
        {
            blogPost = blogPost with
            {
                PublishDate = DateTime.UtcNow,
                LastUpdateDateTime = DateTime.UtcNow,
            };

            newStatus |= request.Status;
        }

        await _repo.UpdateAsync(blogPost with
        {
            Status = newStatus,
            LastUpdateDateTime = DateTime.UtcNow
        }, cancellationToken);

        return Unit.Value;
    }
}
