namespace BlogEngine.Web.Core.Interfaces;

public interface IBlogEngineApiClient
{
    Task<Guid> CreateUserAsync(RegisterViewModel registration, CancellationToken cancellationToken = default);
    Task<JwtToken> AuthenticateAsync(LoginViewModel credentials, CancellationToken cancellationToken = default);
    Task<PaginatedApiResult<BlogPostViewModel>> GetPublishedBlogPostsAsync(int page, int itemsPerPage, CancellationToken cancellationToken = default);
    Task<bool> IsHealthyAsync(CancellationToken cancellationToken = default);
}
