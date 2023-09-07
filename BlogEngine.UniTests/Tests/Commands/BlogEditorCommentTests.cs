namespace BlogEngine.UniTests.Tests.Commands;

public sealed class BlogEditorCommentTests
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldCreateEditorComment()
    {
        // Arrange
        var fakeEditorId = Guid.NewGuid();
        var fakeBlogPostId = Guid.NewGuid();
        var blogEditorComment = new BlogEditorComment { BlogPostId = fakeBlogPostId, EditorId = fakeEditorId, Content = "Needs more detail." };

        var editorCommentRepoMock = new Mock<IBasePersistanceRepository<BlogEditorComment>>();

        var httpAccessorMock = new Mock<IHttpContextAccessor>();
        httpAccessorMock.Setup(x => x.HttpContext.Items["UserId"]).Returns(fakeEditorId.ToString());

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<BlogEditorComment>(It.IsAny<CreateBlogEditorCommentCommand>())).Returns(blogEditorComment);

        var handler = new CreateBlogEditorCommentCommandHandler(editorCommentRepoMock.Object, mapperMock.Object, httpAccessorMock.Object);

        // Act
        var result = await handler.Handle(new CreateBlogEditorCommentCommand("Needs more detail.", fakeBlogPostId), 
            CancellationToken.None);

        // Assert
        editorCommentRepoMock.Verify(repo => repo.AddAsync(It.Is<BlogEditorComment>(comment => comment.Content == "Needs more detail."), CancellationToken.None), Times.Once);
    }
}
