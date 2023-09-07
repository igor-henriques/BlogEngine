using BlogEngine.Domain.Commands.BlogEditorComment.Create;

namespace BlogEngine.API.Endpoints;

public static class BlogPostEndpoints
{
    public static void ConfigureBlogPostEndpoints(this WebApplication app)
    {
        app.MapPost(Routes.BlogPost.Create, async (
           [FromBody] CreateBlogPostCommand command,
           [FromServices] IMediator mediator,
           CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        }).WithTags(SwaggerTags.BlogPost).RequireAuthorization(RoleNames.Writer);

        app.MapPut(Routes.BlogPost.Put, async (
           [FromBody] PutBlogPostCommand command,
           [FromServices] IMediator mediator,
           CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        }).WithTags(SwaggerTags.BlogPost).RequireAuthorization(RoleNames.Writer);

        app.MapPatch(Routes.BlogPost.PatchStatus, async (
           [FromBody] UpdateBlogPostStatusCommand command,
           [FromServices] IMediator mediator,
           CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        }).WithTags(SwaggerTags.BlogPost).RequireAuthorization(RoleNames.Editor);

        app.MapGet(Routes.BlogPost.GetPublishedPostsPaginated, async (
           [FromQuery] int? pageNumber,
           [FromQuery] int? itemsPerPage,
           [FromServices] IMediator mediator,
           CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new GetPublishedPostsPaginatedQuery(
                pageNumber.GetValueOrDefault(), 
                itemsPerPage.GetValueOrDefault()), 
            cancellationToken);

            return Results.Ok(response);
        }).WithTags(SwaggerTags.BlogPost);

        app.MapPost(Routes.BlogPost.AddComment, async (
           [FromBody] CreateBlogCommentCommand command,
           [FromServices] IMediator mediator,
           CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        }).WithTags(SwaggerTags.BlogPost);

        app.MapPost(Routes.BlogPost.AddEditorComment, async (
           [FromBody] CreateBlogEditorCommentCommand command,
           [FromServices] IMediator mediator,
           CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        }).WithTags(SwaggerTags.BlogPost).RequireAuthorization(RoleNames.Editor);

        app.MapGet(Routes.BlogPost.GetByAuthor, async (      
           [FromServices] IMediator mediator,
           CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new GetBlogPostByAuthorQuery(),cancellationToken);
            return Results.Ok(response);
        }).WithTags(SwaggerTags.BlogPost).RequireAuthorization("EditorOrWriter");

        app.MapGet(Routes.BlogPost.GetByStatus, async (
           [FromQuery] EPublishStatus statuses,
           [FromServices] IMediator mediator,
           CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new GetBlogPostByStatusQuery(statuses), cancellationToken);
            return Results.Ok(response);
        }).WithTags(SwaggerTags.BlogPost).RequireAuthorization(RoleNames.Editor);

        app.MapPut(Routes.BlogPost.ReproveBlogPost, async (
           [FromBody] ReproveBlogPostCommand command,
           [FromServices] IMediator mediator,
           CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        }).WithTags(SwaggerTags.BlogPost).RequireAuthorization(RoleNames.Editor);
    }
}