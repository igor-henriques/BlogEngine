namespace BlogEngine.API.Middlewares;

public sealed class AuthenticatedUserIdResolverMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticatedUserIdResolverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {        
        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

        if (userIdClaim != null)
        {            
            context.Items["UserId"] = userIdClaim.Value;
        }
        
        await _next(context);
    }
}
