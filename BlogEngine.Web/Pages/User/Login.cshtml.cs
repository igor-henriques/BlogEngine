namespace BlogEngine.Web.Pages.User;

public sealed class LoginModel : PageModel
{
    [BindProperty]
    public LoginViewModel LoginViewModel { get; set; }

    private readonly IBlogEngineApiClient _apiClient;

    public LoginModel(IBlogEngineApiClient blogEngineApiClient)
    {
        _apiClient = blogEngineApiClient;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _apiClient.AuthenticateAsync(LoginViewModel, cancellationToken);
            Response.Cookies.Append(nameof(JwtToken), JsonConvert.SerializeObject(token), new CookieOptions()
            {
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, LoginViewModel.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = token.ExpiresAt,                
            };
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
        catch (TaskCanceledException)
        {
            return Page();
        }

        return Redirect("/Index");
    }
}
