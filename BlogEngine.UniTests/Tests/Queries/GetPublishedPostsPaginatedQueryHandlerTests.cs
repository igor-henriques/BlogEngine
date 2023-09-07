namespace BlogEngine.UniTests.Tests.Queries;

public sealed class GetPublishedPostsPaginatedQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldReturnMappedBlogPosts()
    {
        // Arrange
        var fakeBlogPosts = new List<BlogPost>
        {
            new BlogPost { Id = Guid.NewGuid(), Title = "Post1", Content = "Content1", PublishDate = DateTime.Now, Status = EPublishStatus.Published },
            new BlogPost { Id = Guid.NewGuid(), Title = "Post2", Content = "Content2", PublishDate = DateTime.Now, Status = EPublishStatus.Published }
        };

        var fakeBlogPostDtos = fakeBlogPosts.Select(post => new BlogPostDto { Id = post.Id, Title = post.Title, Content = post.Content, PublishDate = post.PublishDate.GetValueOrDefault(), Status = post.Status });

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
