namespace BlogEngine.UniTests.Tests.Commands;

public sealed class BlogCommentTests
{
    [Fact]
    public async Task Handle_WhenCalledWithPublishedPost_ShouldCreateComment()
    {
        // Arrange
        var fakeBlogPostId = Guid.NewGuid();
        var fakeAuthorId = Guid.NewGuid();
        var blogPost = new BlogPost("Title", "Content", DateTime.UtcNow, DateTime.UtcNow, EPublishStatus.Published, fakeAuthorId, null);
        var blogComment = new BlogComment { BlogPostId = fakeBlogPostId, Username = "1", Email = "test@test.com", Content = "Great post!" };

        var commentRepoMock = new Mock<IBasePersistanceRepository<BlogComment>>();
        var blogPostRepoMock = new Mock<IReadOnlyBaseRepository<BlogPost>>();
        blogPostRepoMock.Setup(repo => repo.GetUniqueAsync(fakeBlogPostId, It.IsAny<CancellationToken>())).ReturnsAsync(blogPost);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<BlogComment>(It.IsAny<CreateBlogCommentCommand>())).Returns(blogComment);

        var handler = new CreateBlogCommentCommandHandler(commentRepoMock.Object, blogPostRepoMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(new CreateBlogCommentCommand
        {
            BlogPostId = fakeBlogPostId,
            Username = "1",
            Email = "test@test.com",
            Content = "Great post!"
        }, CancellationToken.None);

        // Assert
        commentRepoMock.Verify(repo => repo.AddAsync(It.Is<BlogComment>(comment => comment.Content == "Great post!"), CancellationToken.None), Times.Once);
    }
}
