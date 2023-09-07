using static BlogEngine.Core.Shared.Constants;

namespace BlogEngine.IoC.Injectors;

public static class ConfigureAuthenticationAuthorization
{
    public static void AddCustomAuthentication(
        this IServiceCollection services,
        string jwtBearerKey)
    {
        ArgumentValidator.ThrowIfNullOrEmpty(jwtBearerKey);

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
            jwt.RequireHttpsMetadata = true;
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtBearerKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });
    }

    public static void AddCustomAuthorization(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Constants.RoleNames.Editor, policy => policy.RequireClaim(ClaimTypes.Role, EUserRole.Editor.ToString()));
            options.AddPolicy(Constants.RoleNames.Writer, policy => policy.RequireClaim(ClaimTypes.Role, EUserRole.Writer.ToString()));
            options.AddPolicy("EditorOrWriter", policy =>
            policy.RequireAssertion(context =>
                context.User.HasClaim(c =>
                    (c.Type == ClaimTypes.Role) &&
                    (c.Value == RoleNames.Editor || c.Value == RoleNames.Writer))
            ));
        });
    }
}
