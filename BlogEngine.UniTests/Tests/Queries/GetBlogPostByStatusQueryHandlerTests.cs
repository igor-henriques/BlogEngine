namespace BlogEngine.UniTests.Tests.Queries;

public sealed class GetBlogPostByStatusQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldReturnBlogPostsByStatus()
    {
        // Arrange
        var fakeStatus = EPublishStatus.Published;
        var fakeBlogPosts = new List<BlogPost>
    {
        new BlogPost { Id = Guid.NewGuid(), Title = "Post1", Content = "Content1", PublishDate = DateTime.Now, Status = fakeStatus },
        new BlogPost { Id = Guid.NewGuid(), Title = "Post2", Content = "Content2", PublishDate = DateTime.Now, Status = fakeStatus }
    };

        var fakeBlogPostDtos = fakeBlogPosts.Select(post => new BlogPostDto { Id = post.Id, Title = post.Title, Content = post.Content, PublishDate = post.PublishDate.GetValueOrDefault(), Status = post.Status });

        var repoMock = new Mock<IBlogPostReadOnlyRepository>();
        repoMock.Setup(repo => repo.GetByStatusAsync(fakeStatus, CancellationToken.None)).ReturnsAsync(fakeBlogPosts);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<BlogPostDto>(It.IsAny<BlogPost>())).Returns<BlogPost>(post => fakeBlogPostDtos.First(dto => dto.Id == post.Id));

        var handler = new GetBlogPostByStatusQueryHandler(repoMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(new GetBlogPostByStatusQuery(fakeStatus), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(fakeBlogPostDtos);
    }

}
