namespace BlogEngine.IoC.Injectors;

public static class ConfigureValidators
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
        services.AddScoped<IValidator<PatchRoleCommand>, PatchRoleCommandValidator>();
        services.AddScoped<IValidator<PatchPasswordCommand>, PatchPasswordCommandValidator>();
        services.AddScoped<IValidator<AuthenticateQuery>, AuthenticateQueryValidator>();
        services.AddScoped<IValidator<CreateBlogPostCommand>, CreateBlogPostCommandValidator>();
        services.AddScoped<IValidator<PutBlogPostCommand>, PutBlogPostCommandValidator>();
        services.AddScoped<IValidator<UpdateBlogPostStatusCommand>, UpdateBlogPostStatusCommandValidator>();
        services.AddScoped<IValidator<CreateBlogCommentCommand>, CreateBlogCommentCommandValidator>();

        return services;
    }
}
