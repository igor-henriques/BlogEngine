using BlogEngine.Domain.Interfaces.Repositories;
using BlogEngine.Infrastructure.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Services.Configure<JsonOptions>(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

    builder.Services.AddApplicationInsightsTelemetry();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwagger();
    builder.Services.AddOptionsConfigurations(builder.Configuration);
    builder.Services.AddGeneralDependencies();
    builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnection")));
    builder.Services.AddMapperProfiles();
    builder.Services.AddValidators();
    builder.Services.AddCqrsHandlers();
    builder.Services.AddCustomAuthentication(builder.Configuration.GetValue<string>("JwtAuthentication:Key"));
    builder.Services.AddCustomAuthorization();
    builder.Services.AddHealthChecks();
    builder.Services.AddCors();

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.UseMiddleware<BlogEngine.API.Middlewares.HttpLoggingDetailsMiddleware>();
    app.UseMiddleware<BlogEngine.API.Middlewares.ExceptionHandlerMiddleware>();
    app.UseMiddleware<BlogEngine.API.Middlewares.AuthenticatedUserIdResolverMiddleware>();

    if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();               
    }    

    app.MapHealthChecks(Routes.Health);
    app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.UseHttpsRedirection();    

    app.ConfigureUserEndpoints();
    app.ConfigureBlogPostEndpoints();

    await SeedAsync();

    await app.RunAsync();

    async Task SeedAsync()
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<IDataSeeder>();

        if (app.Environment.IsDevelopment())
        {
            await context.SeedFakeDataAsync();
        }
        else if (app.Environment.IsProduction())
        {
            await context.SeedInitialProductionDataAsync();
        }
    }
}
catch (Exception ex)
{
    Log.Fatal(ex.ToString());
    throw;
}
finally
{
    Log.CloseAndFlush();
}