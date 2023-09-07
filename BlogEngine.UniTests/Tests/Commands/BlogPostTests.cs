namespace BlogEngine.UniTests.Tests.Commands;

public sealed class BlogPostTests
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldCreateBlogPostWithSubmmitedStatus()
    {
        // Arrange
        var fakeTitle = "Fake Title";
        var fakeContent = "Fake Content";
        var fakeAuthorId = Guid.NewGuid();

        var blogPost = new BlogPost(fakeTitle, fakeContent, null, null, EPublishStatus.Submmited, fakeAuthorId, null);

        var repoMock = new Mock<IBasePersistanceRepository<BlogPost>>();
        var mapperMock = new Mock<IMapper>();

        mapperMock.Setup(mapper => mapper.Map<BlogPost>(It.IsAny<CreateBlogPostCommand>()))
                  .Returns<CreateBlogPostCommand>(x => blogPost);

        var httpAccessorMock = new Mock<IHttpContextAccessor>();
        httpAccessorMock.Setup(x => x.HttpContext.Items["UserId"]).Returns(fakeAuthorId.ToString());

        var handler = new CreateBlogPostCommandHandler(repoMock.Object, mapperMock.Object, httpAccessorMock.Object);

        // Act
        var result = await handler.Handle(new CreateBlogPostCommand
        {
            Title = fakeTitle,
            Content = fakeContent
        }, CancellationToken.None);

        // Assert
        repoMock.Verify(repo => repo.AddAsync(
            It.Is<BlogPost>(bp => bp.Title == fakeTitle
                             && bp.Content == fakeContent
                              && bp.Status == EPublishStatus.Submmited),
            CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenPostIsNotPublishedOrSubmmited_ShouldUpdatePost()
    {
        // Arrange
        var fakeId = Guid.NewGuid();
        var fakeTitle = "New Title";
        var fakeContent = "New Content";
        var existingPost = new BlogPost("Old Title", "Old Content", null, null, EPublishStatus.Approved, Guid.NewGuid(), null);

        var repoMock = new Mock<IBasePersistanceRepository<BlogPost>>();
        repoMock.Setup(repo => repo.GetUniqueAsync(fakeId, It.IsAny<CancellationToken>())).ReturnsAsync(existingPost);

        var handler = new PutBlogPostCommandHandler(repoMock.Object);

        // Act
        await handler.Handle(new PutBlogPostCommand
        {
            BlogPostId = fakeId,
            Title = fakeTitle,
            Content = fakeContent
        }, CancellationToken.None);

        // Assert
        repoMock.Verify(repo => repo.UpdateAsync(
            It.Is<BlogPost>(bp => bp.Title == fakeTitle && bp.Content == fakeContent),
            CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenPostIsPublishedOrSubmmited_ShouldThrowException()
    {
        // Arrange
        var fakeId = Guid.NewGuid();
        var existingPost = new BlogPost("Old Title", "Old Content", null, null, EPublishStatus.Published, Guid.NewGuid(), null);

        var repoMock = new Mock<IBasePersistanceRepository<BlogPost>>();
        repoMock.Setup(repo => repo.GetUniqueAsync(fakeId, It.IsAny<CancellationToken>())).ReturnsAsync(existingPost);

        var handler = new PutBlogPostCommandHandler(repoMock.Object);

        // Act
        Func<Task> act = async () => await handler.Handle(new PutBlogPostCommand
        {
            BlogPostId = fakeId,
            Title = "New Title",
            Content = "New Content"
        }, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Only not published or submmited posts can be editted");
    }

    [Fact]
    public async Task Handle_WhenUserIsEditor_ShouldReprovePost()
    {
        // Arrange
        var fakeEditorId = Guid.NewGuid();
        var fakePostId = Guid.NewGuid();
        var fakeReason = "Not good enough";

        var editor = new User { Role = EUserRole.Editor };
        var existingPost = new BlogPost("Title", "Content", null, null, EPublishStatus.Approved | EPublishStatus.Submmited, Guid.NewGuid(), null);

        var userRepoMock = new Mock<IReadOnlyBaseRepository<User>>();
        var blogPostRepoMock = new Mock<IBasePersistanceRepository<BlogPost>>();
        var mediatorMock = new Mock<IMediator>();

        userRepoMock.Setup(repo => repo.GetUniqueAsync(fakeEditorId, It.IsAny<CancellationToken>())).ReturnsAsync(editor);
        blogPostRepoMock.Setup(repo => repo.GetUniqueAsync(fakePostId, It.IsAny<CancellationToken>())).ReturnsAsync(existingPost);

        var httpAccessorMock = new Mock<IHttpContextAccessor>();
        httpAccessorMock.Setup(x => x.HttpContext.Items["UserId"]).Returns(fakeEditorId.ToString());

        var handler = new ReproveBlogPostCommandHandler(mediatorMock.Object, userRepoMock.Object, blogPostRepoMock.Object, httpAccessorMock.Object);

        // Act
        await handler.Handle(new ReproveBlogPostCommand
        {
            BlogPostId = fakePostId,
            Reason = fakeReason
        }, CancellationToken.None);

        // Assert
        blogPostRepoMock.Verify(repo => repo.UpdateAsync(
            It.Is<BlogPost>(bp => bp.Status.HasFlag(EPublishStatus.Reproved)),
            CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenUserIsNotEditor_ShouldThrowException()
    {
        // Arrange
        var fakeEditorId = Guid.NewGuid();

        var notEditor = new User { Role = EUserRole.Writer };

        var userRepoMock = new Mock<IReadOnlyBaseRepository<User>>();
        userRepoMock.Setup(repo => repo.GetUniqueAsync(fakeEditorId, It.IsAny<CancellationToken>())).ReturnsAsync(notEditor);

        var blogPostRepoMock = new Mock<IBasePersistanceRepository<BlogPost>>();
        var mediatorMock = new Mock<IMediator>();

        var httpAccessorMock = new Mock<IHttpContextAccessor>();
        httpAccessorMock.Setup(x => x.HttpContext.Items["UserId"]).Returns(fakeEditorId.ToString());

        var handler = new ReproveBlogPostCommandHandler(mediatorMock.Object, userRepoMock.Object, blogPostRepoMock.Object, httpAccessorMock.Object);

        // Act
        Func<Task> act = async () => await handler.Handle(new ReproveBlogPostCommand
        {
            BlogPostId = Guid.NewGuid(),
            Reason = "Not good enough"
        }, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage($"User * is not an editor");
    }

    [Fact]
    public async Task Handle_UpdateStatusToApproved_ShouldRemoveReprovedAndSetApproved()
    {
        // Arrange
        var fakePostId = Guid.NewGuid();
        var existingPost = new BlogPost("Title", "Content", null, null, EPublishStatus.Reproved, Guid.NewGuid(), null);

        var blogPostRepoMock = new Mock<IBasePersistanceRepository<BlogPost>>();
        blogPostRepoMock.Setup(repo => repo.GetUniqueAsync(fakePostId, It.IsAny<CancellationToken>())).ReturnsAsync(existingPost);

        var handler = new UpdateBlogPostStatusCommandHandler(blogPostRepoMock.Object);

        // Act
        await handler.Handle(new UpdateBlogPostStatusCommand
        {
            BlogPostId = fakePostId,
            Status = EPublishStatus.Approved,
        }, CancellationToken.None);

        // Assert
        blogPostRepoMock.Verify(repo => repo.UpdateAsync(
            It.Is<BlogPost>(bp => bp.Status.HasFlag(EPublishStatus.Approved) && !bp.Status.HasFlag(EPublishStatus.Reproved)),
            CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_UpdateStatusToReproved_ShouldSetReproved()
    {
        // Arrange
        var fakePostId = Guid.NewGuid();
        var existingPost = new BlogPost("Title", "Content", null, null, EPublishStatus.Approved, Guid.NewGuid(), null);

        var blogPostRepoMock = new Mock<IBasePersistanceRepository<BlogPost>>();
        blogPostRepoMock.Setup(repo => repo.GetUniqueAsync(fakePostId, It.IsAny<CancellationToken>())).ReturnsAsync(existingPost);

        var handler = new UpdateBlogPostStatusCommandHandler(blogPostRepoMock.Object);

        // Act
        await handler.Handle(new UpdateBlogPostStatusCommand
        {
            BlogPostId = fakePostId,
            Status = EPublishStatus.Reproved,
        }, CancellationToken.None);

        // Assert
        blogPostRepoMock.Verify(repo => repo.UpdateAsync(
            It.Is<BlogPost>(bp => bp.Status.HasFlag(EPublishStatus.Reproved)),
            CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_TryToUpdateStatusToPublishedWhenReproved_ShouldNotUpdate()
    {
        // Arrange
        var fakePostId = Guid.NewGuid();
        var existingPost = new BlogPost("Title", "Content", null, null, EPublishStatus.Reproved, Guid.NewGuid(), null);

        var blogPostRepoMock = new Mock<IBasePersistanceRepository<BlogPost>>();
        blogPostRepoMock.Setup(repo => repo.GetUniqueAsync(fakePostId, It.IsAny<CancellationToken>())).ReturnsAsync(existingPost);

        var handler = new UpdateBlogPostStatusCommandHandler(blogPostRepoMock.Object);

        // Act
        await handler.Handle(new UpdateBlogPostStatusCommand
        {
            BlogPostId = fakePostId,
            Status = EPublishStatus.Published,
        }, CancellationToken.None);

        // Assert
        blogPostRepoMock.Verify(repo => repo.UpdateAsync(
            It.Is<BlogPost>(bp => bp.Status.HasFlag(EPublishStatus.Reproved)),
            CancellationToken.None), Times.Once);
    }
}
