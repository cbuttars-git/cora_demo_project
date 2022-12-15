// 
//    Project:  model.applicationsettings
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

using System.Reflection;
using Navient.Presentation.Configuration.Model.Serilog;
using static Navient.Presentation.Configuration.Model.Constant;

namespace Navient.Presentation.Configuration.Model;

/// <summary>
/// </summary>
public static class Extension
{
  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyApi" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("Api") </param>
  /// <param name="assembly">Recommend <see cref="Assembly.GetExecutingAssembly()" /> value.</param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyApi" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyApi ApiFactory(this IConfigurationSection parameter, Assembly assembly = null)
  {
    return new ApiModel(parameter, assembly);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyApplication" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("Application") </param>
  /// <param name="assembly">Recommend <see cref="Assembly.GetExecutingAssembly()" /> value.</param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyApplication" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyApplication ApplicationFactory(this IConfigurationSection parameter, Assembly assembly)
  {
    return new ApplicationModel(parameter, assembly);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyApplicationSetting" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.</param>
  /// <param name="assembly">Recommend <see cref="Assembly.GetExecutingAssembly()" /> value.</param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyApplicationSetting" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyApplicationSetting ApplicationSetting(this IConfiguration parameter, Assembly assembly = null)
  {
    return new ApplicationSettingsModel(parameter, assembly);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyArguments" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("Args") </param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyArguments" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyArguments ArgumentsFactory(this IConfigurationSection parameter)
  {
    return new ArgumentsModel(parameter);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyConfigure" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("Configure") </param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyConfigure" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyConfigure ConfigureFactory(this IConfigurationSection parameter)
  {
    return new ConfigureModel(parameter);
  }


  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyContact" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("Contact") </param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyContact" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyContact ContactFactory(this IConfigurationSection parameter)
  {
    return new ContactModel(parameter);
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <returns></returns>
  public static IReadOnlyDictionary<string, string> ConnectionStringsFactory(this IConfiguration parameter)
  {
    var configurations = parameter.GetSection(ApplicationSettings.JSON_PROPERTY_NAME_CONNECTION_STRINGS)?.GetChildren()?.ToList();
    return configurations?.Any() is not true ? null : configurations.Select(ConnectionStringFactory).ToDictionary(item => item.Key, item => item.Value);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyLogging" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("Logging") </param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyLogging" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyLogging LoggingFactory(this IConfigurationSection parameter)
  {
    return new LoggingModel(parameter);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyLogLevel" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("LogLevel") </param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyLogLevel" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyLogLevel LogLevelFactory(this IConfigurationSection parameter)
  {
    return new LogLevelModel(parameter);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyMinimumLevel" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("WriteTo") </param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyMinimumLevel" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyMinimumLevel MinimumLevelFactory(this IConfigurationSection parameter)
  {
    return new MinimumLevelModel(parameter);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyOverride" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("Override") </param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyOverride" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyOverride OverriderFactory(this IConfigurationSection parameter)
  {
    return new OverrideModel(parameter);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlySerilog" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("Serilog") </param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlySerilog" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlySerilog SerilogFactory(this IConfigurationSection parameter)
  {
    return new SerilogModel(parameter);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlySwagger" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("Swagger") </param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlySwagger" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlySwagger SwaggerFactory(this IConfigurationSection parameter)
  {
    return new SwaggerModel(parameter);
  }

  /// <summary>
  ///   Factory extension method used to convert appsettings.json into an object representation of <see cref="IReadOnlyWriteTo" />
  /// </summary>
  /// <param name="parameter">Use <see cref="IConfigurationSection" />.GetSection("LogLevel") </param>
  /// <returns>
  ///   New instance of <see cref="IReadOnlyWriteTo" /> based on the merged appsettings.json Configuration file.
  /// </returns>
  public static IReadOnlyWriteTo WriteToFactory(this IConfigurationSection parameter)
  {
    return new WriteToModel(parameter);
  }

  private static KeyValuePair<string, string> ConnectionStringFactory(this IConfigurationSection parameter)
  {
    return new KeyValuePair<string, string>(parameter.GetValue<string>(ConnectionString.JSON_PROPERTY_NAME_NAME),
        parameter.GetValue<string>(ConnectionString.JSON_PROPERTY_NAME_VALUE));
  }
}