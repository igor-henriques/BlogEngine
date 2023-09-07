namespace BlogEngine.IoC.Injectors;

public static class ConfigureCqrsHandlers
{
    public static IServiceCollection AddCqrsHandlers(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Entity<>).Assembly)
        .AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>)));

        return services;
    }
}
