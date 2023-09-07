namespace BlogEngine.API.Middlewares;

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
            await _next(context);
        }
        catch (InvalidCredentialsException ex)
        {
            await HandleAsync(context, ex);
        }
        catch (ValidationException ex)
        {
            await HandleAsync(context, ex);
        }
        catch (InvalidOperationException ex)
        {
            await HandleAsync(context, ex);
        }   
        catch (Exception ex)
        {
            await HandleAsync(context, ex);
        }
    }

    private static async Task HandleAsync(HttpContext context, ValidationException ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var errorMessage = JsonSerializer.Serialize(
            new
            {
                Messages = ex.Message,
                context.Response.StatusCode
            });

        await context.Response.WriteAsync(errorMessage);
    }

    private static async Task HandleAsync(HttpContext context, InvalidCredentialsException ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Response.ContentType = "application/json";

        var errorMessage = JsonSerializer.Serialize(
            new
            {
                Messages = ex.Message.Split("\n"),
                context.Response.StatusCode
            });

        await context.Response.WriteAsync(errorMessage);
    }

    private static async Task HandleAsync(HttpContext context, InvalidOperationException ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var errorMessage = JsonSerializer.Serialize(
            new
            {
                Messages = ex.Message.Split("\n"),
                context.Response.StatusCode
            });

        await context.Response.WriteAsync(errorMessage);
    }

    private static async Task HandleAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var errorMessage = JsonSerializer.Serialize(
            new
            {
                Messages = ex.Message,
                context.Response.StatusCode
            });

        await context.Response.WriteAsync(errorMessage);
    }    
}
