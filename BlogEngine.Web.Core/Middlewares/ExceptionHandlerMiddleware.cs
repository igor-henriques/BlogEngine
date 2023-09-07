namespace BlogEngine.Web.Core.Middlewares;

public sealed class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (AuthenticationException)
        {
            context.Response.Redirect("/User/Login");
        }
        catch (Exception)
        {
            context.Response.Redirect("/Error");
        }
    }
}
