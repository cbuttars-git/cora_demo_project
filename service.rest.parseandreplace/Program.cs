// 
//    Project:  rest.parseandreplace
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
public class Program
{
  internal static ILogger Logger { get; private set; }

  /// <summary>
  /// </summary>
  /// <param name="args"></param>
  /// <returns></returns>
  public static IHostBuilder CreateHostBuilder(string[] args)
  {
    return Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
          Env.Load(Path.Combine(hostingContext.HostingEnvironment.ContentRootPath, Application.ENVIRONMENT_FILE_EXTENSION));
          config.AddEnvironmentVariables();
        })
        .UseSerilogLogger()
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
  }

  /// <summary>
  /// </summary>
  /// <param name="args"></param>
  /// <returns></returns>
  public static async Task<int> Main(string[] args)
  {
    Logger = CreateSerilogStartUpLogger().LogStartUpMessage(Application.DEFAULT_APPLICATION_NAME);


    try
    {
      await CreateHostBuilder(args).Build().RunAsync().ConfigureAwait(false);
      return 0;
    }
    catch (Exception ex)
    {
      Log.Logger.LogFatalException(nameof(Program), nameof(Main), ex);
      return 1;
    }
    finally
    {
      Log.Logger.LogApplicationIsShuttingDown(Application.DEFAULT_APPLICATION_NAME);
      await Log.CloseAndFlushAsync();
    }
  }
}