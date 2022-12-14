// 
//    Project:  extension.serilog
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
public static class SerilogStartupExtension
{
  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <returns></returns>
  public static IHostBuilder UseSerilogLogger(this IHostBuilder parameter)
  {
    return parameter.UseSerilog((context, services, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console());
  }

  /// <summary>
  /// </summary>
  /// <returns></returns>
  public static ILogger CreateSerilogStartUpLogger()
  {
    return new LoggerConfiguration()
        .MinimumLevel.Override(OVERRIDE_MICROSOFT_NAME, LogEventLevel.Information) // Not sure this is going to do what I want it to do.
#if DEBUG
        .MinimumLevel.Verbose()
#else
        .MinimumLevel.Information()
#endif
        .Enrich.FromLogContext()
        .Enrich.WithEnvironmentName()
        .Enrich.WithMachineName()
        .Enrich.WithThreadId()
        .WriteTo.Console(outputTemplate: DEFAULT_LOGGING_OUTPUT_TEMPLATE)
        .CreateBootstrapLogger();
  }
}