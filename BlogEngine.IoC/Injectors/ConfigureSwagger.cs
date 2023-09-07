namespace BlogEngine.IoC.Injectors;

public static class ConfigureSwagger
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BlogEngine API",
                Version = "v1",
                Description = "Provide full control over BlogEngine operation",
                Contact = new OpenApiContact() { Name = "Ironside Productions" }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Use 'Bearer <TOKEN>'",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                }, Array.Empty<string>()
            }
        });

            c.EnableAnnotations();
        });

        return services;
    }
}
