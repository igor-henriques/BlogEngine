namespace BlogEngine.API.Endpoints;

public static class UserEndpoints
{
    public static void ConfigureUserEndpoints(this WebApplication app)
    {
        app.MapPost(Routes.User.Authenticate, async (
            [FromBody] AuthenticateQuery authenticateUserCommand,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(authenticateUserCommand, cancellationToken);
            return Results.Ok(response);
        }).WithTags(SwaggerTags.User);

        app.MapPost(Routes.User.Create, async (
            [FromBody] CreateUserCommand command,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        }).WithTags(SwaggerTags.User);

        app.MapPatch(Routes.User.PatchPassword, async (
            [FromBody] PatchPasswordCommand command,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags(SwaggerTags.User);

        app.MapPatch(Routes.User.PatchRole, async (
            [FromBody] PatchRoleCommand command,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags(SwaggerTags.User);
    }
}
