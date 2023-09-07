namespace BlogEngine.Web.Pages.User;

public sealed class RegisterModel : PageModel
{
    [BindProperty]
    public RegisterViewModel RegisterViewModel { get; set; }

    private readonly IBlogEngineApiClient _apiClient;

    public RegisterModel(IBlogEngineApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _ = await _apiClient.CreateUserAsync(RegisterViewModel, cancellationToken);

        return Redirect("/User/Login");        
    }
}
