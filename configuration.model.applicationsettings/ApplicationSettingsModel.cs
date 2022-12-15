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

using System;
using System.Reflection;
using Navient.Presentation.Configuration.Model.Serilog;
using static Navient.Presentation.Configuration.Model.Constant.ApplicationSettings;

namespace Navient.Presentation.Configuration.Model;

/// <summary>
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class ApplicationSettingsModel : JsonBase<ApplicationSettingsModel>, IReadOnlyApplicationSetting
{
  /// <summary>
  /// </summary>
  [JsonConstructor]
  public ApplicationSettingsModel()
  {
  }

  /// <summary>
  /// </summary>
  /// <param name="configuration">Required parameter</param>
  /// <param name="assembly">Optional parameter</param>
  internal ApplicationSettingsModel(IConfiguration configuration, Assembly assembly = null)
  {
    Application = configuration.GetSection(JSON_PROPERTY_NAME_APPLICATION).ApplicationFactory(assembly);
    Logging = configuration.GetSection(JSON_PROPERTY_NAME_LOGGING).LoggingFactory();
    Serilog = configuration.GetSection(JSON_PROPERTY_NAME_SERILOG).SerilogFactory();
    AllowedHosts = configuration.GetValue<string>(JSON_PROPERTY_NAME_ALLOWED_HOSTS);
    Urls = configuration.GetValue<string>(JSON_PROPERTY_NAME_URLS);
    ConnectionStrings = configuration.ConnectionStringsFactory();
  }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_APPLICATION)]
  public IReadOnlyApplication Application { get; private set; }

  /// <summary>
  /// </summary>
  public IReadOnlyDictionary<string, string> ConnectionStrings { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_LOGGING)]
  public IReadOnlyLogging Logging { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_SERILOG)]
  public IReadOnlySerilog Serilog { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_ALLOWED_HOSTS)]
  public string AllowedHosts { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_URLS)]
  public string Urls { get; private set; }

  /// <summary>
  /// </summary>
  /// <param name="connectionStringName"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public string GetConnectionString(string connectionStringName)
  {
    if (ConnectionStrings?.Any() is not true) throw new ArgumentOutOfRangeException(nameof(connectionStringName), @"ConnectionStrings are not configured; check appSetting.json");

    return ConnectionStrings.FirstOrDefault(item => item.Key.Equals(connectionStringName)).Value;
  }
}