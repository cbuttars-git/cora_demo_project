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

using System.Diagnostics;
using System.Reflection;
using static Navient.Presentation.Configuration.Model.Constant.Api;

namespace Navient.Presentation.Configuration.Model;

/// <summary>
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class ApiModel : JsonBase<ApiModel>, IReadOnlyApi
{
  /// <summary>
  /// </summary>
  [JsonConstructor]
  public ApiModel()
  {
  }

  /// <summary>
  /// </summary>
  /// <param name="configurationSection"></param>
  internal ApiModel(IConfigurationSection configurationSection)
  {
    AssemblyVersion = configurationSection.GetValue<string>(JSON_PROPERTY_NAME_ASSEMBLY_VERSION);
    Contact = configurationSection.GetSection(JSON_PROPERTY_NAME_CONTACT).ContactFactory();
    DeprecationMessage = configurationSection.GetValue<string>(JSON_PROPERTY_DEPRECATION_MESSAGE);
    Description = configurationSection.GetValue<string>(JSON_PROPERTY_NAME_DESCRIPTION);
    LatestApiVersion = configurationSection.GetValue<string>(JSON_PROPERTY_NAME_LATEST_API_VERSION);
    Title = configurationSection.GetValue<string>(JSON_PROPERTY_NAME_TITLE);
  }

  /// <summary>
  /// </summary>
  /// <param name="configurationSection"></param>
  /// <param name="assembly"></param>
  public ApiModel(IConfigurationSection configurationSection, Assembly assembly) : this(configurationSection)
  {
    AssemblyVersion = assembly is not null ? FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion : JSON_PROPERTY_ASSEMBLY_VERSION;
  }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_CONTACT)]
  public IReadOnlyContact Contact { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_ASSEMBLY_VERSION)]
  public string AssemblyVersion { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_DEPRECATION_MESSAGE)]
  public string DeprecationMessage { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_DESCRIPTION)]
  public string Description { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_LATEST_API_VERSION)]
  public string LatestApiVersion { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_TITLE)]
  public string Title { get; private set; }
}