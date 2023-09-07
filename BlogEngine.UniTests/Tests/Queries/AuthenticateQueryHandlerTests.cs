namespace BlogEngine.UniTests.Tests.Queries;

public sealed class AuthenticateQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenCalledWithValidCredentials_ShouldReturnToken()
    {
        var fakePassword = "ValidPassword1!";
        var fakeHashedPassword = "hashedPassword";
        var fakeToken = new JwtToken
        {
            ExpiresAt = new DateTime(2000, 01, 01),
            Token = "some-random-token"
        };

        var userFaker = new Faker<User>()
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.HashedPassword, f => fakeHashedPassword)
            .RuleFor(u => u.Role, f => f.PickRandom<EUserRole>());

        var fakeUser = userFaker.Generate(1).First();

        var repoMock = new Mock<IReadOnlyBaseRepository<User>>();
        repoMock.Setup(repo => repo.GetUniqueByAsync(It.IsAny<Expression<Func<User, bool>>>(), CancellationToken.None)).ReturnsAsync(fakeUser);

        var passwordHashingServiceMock = new Mock<IPasswordHashingService>();
        passwordHashingServiceMock.Setup(svc => svc.VerifyPassword(fakeHashedPassword, fakePassword)).Returns(true);

        var tokenGeneratorServiceMock = new Mock<ITokenGeneratorService>();
        tokenGeneratorServiceMock.Setup(svc => svc.GenerateToken(It.IsAny<List<Claim>>())).Returns(fakeToken);

        var handler = new AuthenticateQueryHandler(
            passwordHashingServiceMock.Object,
            tokenGeneratorServiceMock.Object,
            repoMock.Object);

        // Act
        var result = await handler.Handle(new AuthenticateQuery
        {
            Email = fakeUser.Email,
            Password = fakePassword
        }, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(fakeToken);
    }

    [Fact]
    public async Task Handle_WhenUserIsNotEditor_ShouldThrowUnauthorizedException()
    {
        // Arrange
        var fakeEditorId = Guid.NewGuid();
        var fakeBlogPostId = Guid.NewGuid();

        var user = new User("fakeName", "editor@example.com", "hashedPassword", EUserRole.Writer); // Not an editor
        var blogPost = new BlogPost("Title", "Content", null, null, EPublishStatus.Submmited, fakeEditorId, null);

        var userRepoMock = new Mock<IReadOnlyBaseRepository<User>>();
        var blogPostRepoMock = new Mock<IBasePersistanceRepository<BlogPost>>();
        var mediatorMock = new Mock<IMediator>();

        userRepoMock.Setup(repo => repo.GetUniqueAsync(fakeEditorId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
        blogPostRepoMock.Setup(repo => repo.GetUniqueAsync(fakeBlogPostId, It.IsAny<CancellationToken>())).ReturnsAsync(blogPost);

        var httpAccessorMock = new Mock<IHttpContextAccessor>();
        httpAccessorMock.Setup(x => x.HttpContext.Items["UserId"]).Returns(fakeEditorId.ToString());

        var handler = new ReproveBlogPostCommandHandler(mediatorMock.Object, userRepoMock.Object, blogPostRepoMock.Object, httpAccessorMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => handler.Handle(new ReproveBlogPostCommand
        {
            BlogPostId = fakeBlogPostId,
            Reason = "Not good enough"
        }, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenBlogPostIsSubmmitedOrPublished_ShouldThrowException()
    {
        // Arrange
        var fakePostId = Guid.NewGuid();
        var blogPost = new BlogPost("Title", "Content", EPublishStatus.Submmited); // Already submmited

        var repoMock = new Mock<IBasePersistanceRepository<BlogPost>>();
        repoMock.Setup(repo => repo.GetUniqueAsync(fakePostId, It.IsAny<CancellationToken>())).ReturnsAsync(blogPost);

        var handler = new PutBlogPostCommandHandler(repoMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(new PutBlogPostCommand
        {
            BlogPostId = fakePostId,
            Title = "New Title",
            Content = "New Content"
        }, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenCalled_ShouldReturnMappedBlogPosts()
    {
        // Arrange
        var fakeBlogPosts = new List<BlogPost> { };
        var fakeBlogPostDtos = fakeBlogPosts.Select(post => new BlogPostDto { });

        var repoMock = new Mock<IBlogPostReadOnlyRepository>();
        repoMock.Setup(repo => repo.GetPublishedPostsPaginatedAsync(It.IsAny<GetPublishedPostsPaginatedQuery>(), CancellationToken.None))
                .ReturnsAsync(new EntityQueryResultPaginated<BlogPost> { Data = fakeBlogPosts });

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<BlogPostDto>(It.IsAny<BlogPost>())).Returns<BlogPost>(post => fakeBlogPostDtos.First(dto => dto.Id == post.Id));

        var handler = new GetPublishedPostsPaginatedQueryHandler(repoMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(new GetPublishedPostsPaginatedQuery(1, 10), CancellationToken.None);

        // Assert
        result.Data.Should().BeEquivalentTo(fakeBlogPostDtos);
    }


}
