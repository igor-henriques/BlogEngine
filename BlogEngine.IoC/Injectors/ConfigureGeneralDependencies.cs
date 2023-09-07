namespace BlogEngine.IoC.Injectors;

public static class ConfigureGeneralDependencies
{
    public static IServiceCollection AddGeneralDependencies(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBasePersistanceRepository<>), typeof(BasePersistanceRepository<>));
        services.AddScoped(typeof(IReadOnlyBaseRepository<>), typeof(ReadOnlyBaseRepository<>));
        services.AddScoped<IBlogPostReadOnlyRepository, BlogPostReadOnlyRepository>();
        services.AddScoped<IDataSeeder, DataSeeder>();
        services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
        services.AddSingleton<ITokenGeneratorService, TokenGeneratorService>();
        return services;
    }
}
