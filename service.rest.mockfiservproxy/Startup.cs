// 
//    Project:  rest.mockfiservproxy
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

namespace Navient.Presentation.Service.Rest;

/// <summary>
/// </summary>
public class Startup
{
  private readonly IReadOnlyApplicationSetting _applicationSetting;

  // private readonly IConfiguration _configuration;
  //private readonly IHostEnvironment _hostEnvironment;
  private readonly ILogger _logger;
  private const string _CLASS_NAME = nameof(Startup);

  /// <summary>
  /// </summary>
  /// <param name="configuration"></param>
  /// <param name="hostEnvironment"></param>
  public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
  {
    _logger = Program.Logger.NotNullOrThrowArgumentNullException(nameof(Program.Logger)).ForContext<Startup>().LogEnteringConstructor(_CLASS_NAME);
    _applicationSetting = configuration.NotNullOrThrowArgumentNullException(nameof(configuration))
        .ApplicationSetting(Assembly.GetExecutingAssembly())
        .NotNullOrThrowArgumentNullException(nameof(_applicationSetting));
    _logger.LogApplicationSettings(() => _applicationSetting.ToJson());
    JsonConvert.DefaultSettings = hostEnvironment.NotNullOrThrowArgumentNullException(nameof(hostEnvironment)).AddDefaultJsonSettings();
    _logger.LogLeavingConstructor(_CLASS_NAME);
  }

  /// <summary>
  /// </summary>
  /// <param name="applicationBuilder"></param>
  /// <param name="logger"></param>
  /// <param name="apiVersionDescriptionProvider"></param>
  public void Configure(IApplicationBuilder applicationBuilder, ILogger<Startup> logger, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
  {
    _logger.LogEnteringMethod(_CLASS_NAME, nameof(Configure));
    applicationBuilder.UseSerilogRequestLogging();
    //applicationBuilder.UseExceptionHandler();
    applicationBuilder.UseHttpsRedirection();
    applicationBuilder.UseRouting();
    applicationBuilder.UseSwaggerWithOptions(apiVersionDescriptionProvider);
    applicationBuilder.UseEndpoints(endpoints =>
    {
      endpoints.MapDefaultControllerRoute();
      endpoints.MapControllers();
      endpoints.MapHealthChecks(Health.ENDPOINT);
    });
    applicationBuilder.UseRequestLocalization();

    logger.LogInformation("Server configuration is completed");
    var address = applicationBuilder.ServerFeatures.Get<IServerAddressesFeature>()?.Addresses.FirstOrDefault();
    if (string.IsNullOrWhiteSpace(address)) return;
    var uri = new Uri(new Uri(address), Application.SWAGGER_RELATIVE_URI);
    _logger.LogApplicationConfigurationComplete()
        .LogApplicationBrowsableUri(uri)
        .LogLeavingMethod(_CLASS_NAME, nameof(Configure));
  }

  /// <summary>
  /// </summary>
  /// <param name="serviceCollection"></param>
  public void ConfigureServices(IServiceCollection serviceCollection)
  {
    _logger.LogEnteringMethod(_CLASS_NAME, nameof(ConfigureServices));
    serviceCollection.AddSingleton(_ => _logger);
    serviceCollection.AddSingleton(_ => _applicationSetting);
    serviceCollection.AddSingleton(_ => _applicationSetting.Application);
    serviceCollection.AddControllers(options => { options.Filters.Add(new ProducesAttribute(MEDIA_TYPE_APPLICATION_JSON)); });
    serviceCollection.AddLoadFiservDataHandler();
    serviceCollection.AddApiVersioning(option =>
        {
          option.ReportApiVersions = true;
          option.AssumeDefaultVersionWhenUnspecified = true;
          option.DefaultApiVersion = new ApiVersion(1, 0);
          option.ApiVersionReader = new UrlSegmentApiVersionReader();
        }
    );
    serviceCollection.AddVersionedApiExplorer(
        options =>
        {
          options.GroupNameFormat = Application.API_VERSIONED_EXPLORER_GROUP_FORMAT;
          options.SubstituteApiVersionInUrl = true;
        });
    serviceCollection.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfiguration>();
    serviceCollection.AddSwagger(_applicationSetting.Application);

    serviceCollection.AddRouting();
    serviceCollection.AddControllers();
    serviceCollection.AddHealthChecks();
    _logger.LogLeavingMethod(_CLASS_NAME, nameof(ConfigureServices));
  }
}