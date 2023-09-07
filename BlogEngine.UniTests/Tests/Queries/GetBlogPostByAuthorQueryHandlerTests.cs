namespace BlogEngine.UniTests.Tests.Queries;

public sealed class GetBlogPostByAuthorQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldReturnBlogPostsByAuthor()
    {
        // Arrange
        var fakeAuthorId = Guid.NewGuid();
        var fakeBlogPosts = new List<BlogPost>
        {
            new BlogPost { Id = Guid.NewGuid(), Title = "Post1", Content = "Content1", PublishDate = DateTime.Now, AuthorId = fakeAuthorId },
            new BlogPost { Id = Guid.NewGuid(), Title = "Post2", Content = "Content2", PublishDate = DateTime.Now, AuthorId = fakeAuthorId }
        };

        var fakeBlogPostDtos = fakeBlogPosts.Select(post => new BlogPostDto 
        { 
            Id = post.Id, 
            Title = post.Title, 
            Content = post.Content, 
            PublishDate = post.PublishDate.GetValueOrDefault()
        });

        var repoMock = new Mock<IBlogPostReadOnlyRepository>();
        repoMock.Setup(repo => repo.GetByAuthorAsync(fakeAuthorId, CancellationToken.None)).ReturnsAsync(fakeBlogPosts);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<BlogPostDto>(It.IsAny<BlogPost>())).Returns<BlogPost>(post => fakeBlogPostDtos.First(dto => dto.Id == post.Id));

        var httpAccessorMock = new Mock<IHttpContextAccessor>();
        httpAccessorMock.Setup(x => x.HttpContext.Items["UserId"]).Returns(fakeAuthorId.ToString());

        var handler = new GetBlogPostByAuthorQueryHandler(repoMock.Object, mapperMock.Object, httpAccessorMock.Object);

        // Act
        var result = await handler.Handle(new GetBlogPostByAuthorQuery(), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(fakeBlogPostDtos);
    }

}
