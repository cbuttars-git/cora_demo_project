using InstantAssistant.Api.Helpers;
using InstantAssistant.Api.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting Instant Assistant");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add logging
    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

    // Add services to the container.
    builder.Services.AddDbContext<DataContext>();
    builder.Services.AddHttpClient<HttpClient>();
    builder.Services.AddCors();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<ILogService, LogService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IClientsInfoService, ClientsInfoService>();
    builder.Services.AddScoped<IAIService, AIService>();
    builder.Services.AddScoped<ISessionService, SessionService>();
    builder.Services.AddScoped<IFeedbackService, FeedbackService>();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
        .AddNegotiate();
    builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });

    var app = builder.Build();

    app.UseHttpsRedirection();

    var origins = builder.Configuration.GetSection("InstantAssistant:AllowedOrigins").Get<string[]>();

    Log.Information($"Allowed Origins: {string.Join(", ", origins.ToArray())}");

    app.UseCors(x => x
        .WithOrigins(origins)
        .WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE")
        .AllowAnyHeader()
        .AllowCredentials());

    app.UseAuthentication();
    app.UseAuthorization();

    // More logging for request logging
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Instant Assistant failed to start!");
}
finally
{
    Log.Information("Shut down of Instant Assistant complete");
    Log.CloseAndFlush();
}