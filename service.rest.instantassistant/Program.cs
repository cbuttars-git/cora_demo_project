// 
//    Project:  rest.instantassistant
//    Created:  07-2022-11
// 
//    COPYRIGHT © 2022 - 2030 Navient Solutions, LLC. All rights reserved
// 
//    COPYRIGHT NOTICE
//    The entire contents of this file and the files that make up the assembly that this file resides in are the express property of Navient Solutions, LLC. All Rights Reserved.
//    None of the contents of this file or assembly may be copied or duplicated in whole or part by any means without express prior agreement in writing or unless
//    specifically noted within this file or copyright notice of this file, assembly, or API.
// 
//    Some of the content contained within this file, assembly or API may be the copyrighted property of others; acknowledgement of those copyrights is hereby given.
//    All such material is used with the proper license or permission of the owner.
// 

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

  builder.Services.AddScoped<IChatTranscriptService, ChatTranscriptService>();
  builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
  builder.Services.AddScoped<IClientInformationService, ClientInformationService>();
  builder.Services.AddScoped<IArtificialIntelligenceService, ArtificialIntelligenceService>();
  builder.Services.AddScoped<ISessionService, SessionService>();
  builder.Services.AddScoped<IFeedbackService, FeedbackService>();

  builder.Services.AddHttpContextAccessor();
  builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
      .AddNegotiate();
  builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });

  var app = builder.Build();

  app.UseHttpsRedirection();

  var origins = builder.Configuration.GetSection("InstantAssistant:AllowedOrigins").Get<string[]>();

  Log.Information("Allowed Origins: {Origins}", string.Join(", ", origins.ToArray()));

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