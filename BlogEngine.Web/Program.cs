using BlogEngine.Web.Core.DelegatingHandlers;
using BlogEngine.Web.Core.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAuthentication:Key"]))
        };
    });
builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IBlogEngineApiClient, BlogEngineApiClient>();
builder.Services.AddTransient<LoggingHandler>();
builder.Services.AddTransient<JwtHeaderHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IBlogEngineApiClient, BlogEngineApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BlogEngineApi:BaseUrl"].ToString());
}).AddHttpMessageHandler<LoggingHandler>()
  .AddHttpMessageHandler<JwtHeaderHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<HealthCheckMiddleware>();
app.MapRazorPages();

await app.RunAsync();
