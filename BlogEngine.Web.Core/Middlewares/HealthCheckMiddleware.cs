namespace BlogEngine.Web.Core.Middlewares;

public sealed class HealthCheckMiddleware
{
    private readonly RequestDelegate _next;

    public HealthCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IBlogEngineApiClient apiClient)
    {
        var isHealthy = await apiClient.IsHealthyAsync();

        if (!isHealthy)
        {
            context.Response.Redirect("/UnderMaintenance");
            return;
        }

        await _next.Invoke(context);
    }
}
