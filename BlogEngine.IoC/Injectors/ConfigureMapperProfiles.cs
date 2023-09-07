namespace BlogEngine.IoC.Injectors;

public static class ConfigureMapperProfiles
{
    public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile));
        services.AddAutoMapper(typeof(BlogPostProfile));        
        services.AddAutoMapper(typeof(BlogCommentProfile));
        services.AddAutoMapper(typeof(BlogEditorCommentProfile));

        return services;
    }
}
