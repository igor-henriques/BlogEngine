namespace BlogEngine.Web.Core.DelegatingHandlers;

public sealed class JwtHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _httpContextAccessor.HttpContext.Request.Cookies[nameof(JwtToken)];

        if (!string.IsNullOrEmpty(token))
        {
            var jwtToken = JsonConvert.DeserializeObject<JwtToken>(token);

            if (jwtToken.ExpiresAt > DateTime.UtcNow)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }            
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
