namespace BlogEngine.Domain.Commands.BlogPost.Put;

public sealed class PutBlogPostCommandHandler : IRequestHandler<PutBlogPostCommand, Unit>
{
    private readonly IBasePersistanceRepository<Entities.BlogPost> _repo;

    public PutBlogPostCommandHandler(IBasePersistanceRepository<Entities.BlogPost> repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(PutBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = await _repo.GetUniqueAsync(request.BlogPostId, cancellationToken) with
        {
            Title = request.Title,
            Content = request.Content,
            LastUpdateDateTime = DateTime.UtcNow
        };

        if (request.SendToReview && blogPost.Status.HasFlag(EPublishStatus.Reproved))
        {
            var status = blogPost.Status;

            status &= ~EPublishStatus.Reproved;
            status |= EPublishStatus.Submmited;

            blogPost = blogPost with
            {
                Status = status,
            };
        }
        else if (blogPost.IsPublishedOrSubmmited)
        {
            throw new InvalidOperationException($"Only not published or submmited posts can be editted");
        }

        await _repo.UpdateAsync(blogPost, cancellationToken);

        return Unit.Value;
    }
}
