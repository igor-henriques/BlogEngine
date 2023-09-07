using BlogEngine.Web.Core.Models;

namespace BlogEngine.Web.Core.Services;

public sealed class BlogEngineApiClient : IBlogEngineApiClient
{
    private readonly HttpClient _httpClient;

    public BlogEngineApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid> CreateUserAsync(RegisterViewModel registration, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(Constants.Routes.User.Create, registration, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);        
    }

    public async Task<JwtToken> AuthenticateAsync(LoginViewModel credentials, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(Constants.Routes.User.Authenticate, credentials, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new AuthenticationException();
        }

        return await response.Content.ReadFromJsonAsync<JwtToken>(cancellationToken: cancellationToken);        
    }

    public async Task<PaginatedApiResult<BlogPostViewModel>> GetPublishedBlogPostsAsync(int page, int itemsPerPage, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            $"{Constants.Routes.BlogPost.GetPublishedPostsPaginated}?pageNumber={page}&itemsPerPage={itemsPerPage}", 
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PaginatedApiResult<BlogPostViewModel>>(cancellationToken: cancellationToken);        
    }

    public async Task<bool> IsHealthyAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(Constants.Routes.Health, cancellationToken);
        return response.IsSuccessStatusCode;
    }
}
