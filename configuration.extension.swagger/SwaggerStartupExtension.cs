// 
//    Project:  extension.swagger
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

namespace Navient.Presentation.Configuration.Extension;

/// <summary>
/// </summary>
public static class SwaggerStartupExtension
{
  /// <summary>
  /// </summary>
  /// <param name="applicationBuilder"></param>
  /// <param name="apiVersionDescriptionProvider"></param>
  /// <returns></returns>
  public static IApplicationBuilder UseSwaggerWithOptions(this IApplicationBuilder applicationBuilder, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
  {
    //if (!application.Swagger.IsEnabled is not true) return applicationBuilder;
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    applicationBuilder.UseSwagger(options =>
        {
          options.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
          {
            if (!httpRequest.Headers.ContainsKey(X_FORWARDED_HOST)) return;

            swaggerDoc.Servers = new List<OpenApiServer>
            {
                new() { Url = $"{httpRequest.Headers[X_FORWARDED_PROTO]}://{httpRequest.Headers[X_FORWARDED_HOST]}{httpRequest.Headers[X_FORWARDED_PREFIX]}" }
            };
          });
        })
        .UseSwaggerUI(options =>
        {
          foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            options.SwaggerEndpoint($"{description.GroupName}{SWAGGER_JSON}", description.GroupName.ToUpperInvariant());
          //options.RoutePrefix = string.Empty;
        });

    return applicationBuilder;
  }

  /// <summary>
  ///   Add Swagger middleware
  /// </summary>
  /// <remarks>
  /// </remarks>
  public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection, IReadOnlyApplication application)
  {
    //if (!application.Swagger.IsEnabled is true) return serviceCollection;
    serviceCollection.AddSwaggerGen(options =>
    {
      //if (application.Swagger.UseAnnotations is true)
      options.EnableAnnotations();
      options.SwaggerDoc(application.Api.Title,
          new OpenApiInfo { Title = application.Api.Title, Version = application.Api.LatestApiVersion });


      // Set the comments path for the Swagger JSON and UI.
      var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
      var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
      if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
    });

    return serviceCollection;
  }
}