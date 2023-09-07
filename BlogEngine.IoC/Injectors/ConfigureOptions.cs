namespace BlogEngine.IoC.Injectors;

public static class ConfigureOptions
{
    public static IServiceCollection AddOptionsConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtAuthenticationOptions>(configuration.GetSection("JwtAuthentication"));            
        services.Configure<InitialSeedDataOptions>(configuration.GetSection("InitialSeedDataOptions"));            

        return services;
    }
}
