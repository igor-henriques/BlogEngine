namespace BlogEngine.Pages;

public sealed class IndexModel : PageModel
{
    private readonly IBlogEngineApiClient _apiClient;

    public IEnumerable<BlogPostViewModel> BlogPosts { get; set; } = Enumerable.Empty<BlogPostViewModel>();

    public bool HasNextPage = false;

    public IndexModel(IBlogEngineApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
    {
        var paginatedResult = await _apiClient.GetPublishedBlogPostsAsync(1, 1, cancellationToken);

        BlogPosts = paginatedResult.Data.AsEnumerable();        
        HasNextPage = paginatedResult.TotalPages > 1;

        return Page();
    }

    public async Task<IActionResult> OnGetLoadPage(int currentPage)
    {        
        var paginatedResult = await _apiClient.GetPublishedBlogPostsAsync(currentPage, 1);        

        return new JsonResult(paginatedResult);
    }
}