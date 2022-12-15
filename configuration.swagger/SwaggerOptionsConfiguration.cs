// 
//    Project:  Configuration.Swagger
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

using Serilog;

namespace Navient.Presentation.Service.Rest.Configuration;

/// <summary>
/// </summary>
public class SwaggerOptionsConfiguration : IConfigureOptions<SwaggerGenOptions>
{
  private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;
  private readonly IReadOnlyApplication _application;
  private readonly IConfiguration _configuration;
  private readonly ILogger _logger;
  private const string _CLASS_NAME = nameof(SwaggerOptionsConfiguration);


  /// <summary>
  /// </summary>
  /// <param name="apiVersionDescriptionProvider"></param>
  /// <param name="configuration"></param>
  /// <param name="application"></param>
  /// <param name="logger"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public SwaggerOptionsConfiguration(IApiVersionDescriptionProvider apiVersionDescriptionProvider, IConfiguration configuration, IReadOnlyApplication application,
      ILogger logger)
  {
    _logger = logger?.ForContext<SwaggerOptionsConfiguration>().LogEnteringConstructor(_CLASS_NAME);
    _apiVersionDescriptionProvider = apiVersionDescriptionProvider.NotNullOrThrowArgumentNullException(nameof(apiVersionDescriptionProvider));
    _configuration = configuration.NotNullOrThrowArgumentNullException(nameof(configuration));
    _application = application.NotNullOrThrowArgumentNullException(nameof(application));
    _logger?.LogLeavingConstructor(_CLASS_NAME);
  }

  private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
  {
    var result = new OpenApiInfo
    {
      Title = $"{_application.Api.Title} {description.ApiVersion}",
      Version = description.ApiVersion.ToString(),
      Description = _application.Api?.Description,
      Contact = _application?.Api?.Contact is not null
            ? new OpenApiContact
            {
              Name = _application.Api.Contact.Name, Email = _application.Api.Contact.Email,
              Url = new Uri(_application.Api.Contact.Url)
            }
            : null,
      License = null,
      TermsOfService = null
    };

    if (description.IsDeprecated) result.Description += "This API version has been deprecated.";

    return result;
  }

  /// <summary>
  /// </summary>
  /// <param name="options"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public void Configure(SwaggerGenOptions options)
  {
    _logger.LogEnteringMethod(_CLASS_NAME, nameof(Configure));
    options.NotNullOrThrowArgumentNullException(nameof(options));

    foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
      options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
    _logger.LogLeavingMethod(_CLASS_NAME, nameof(Configure));
  }
}