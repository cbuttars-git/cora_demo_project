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
using static Navient.Presentation.Configuration.Model.Constant.Application;

namespace Navient.Presentation.Configuration.Model;

/// <summary>
/// </summary>
public class ApplicationModel : JsonBase<ApplicationModel>, IReadOnlyApplication
{
  /// <summary>
  /// </summary>
  [JsonConstructor]
  public ApplicationModel()
  {
  }

  /// <summary>
  /// </summary>
  /// <param name="configurationSection"></param>
  /// <param name="assembly"></param>
  internal ApplicationModel(IConfigurationSection configurationSection, Assembly assembly = null)
  {
    Api = configurationSection.GetSection(JSON_PROPERTY_NAME_API).ApiFactory(assembly);
    Swagger = configurationSection.GetSection(JSON_PROPERTY_NAME_SWAGGER).SwaggerFactory();
  }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_API)]
  public IReadOnlyApi Api { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_SWAGGER)]
  public IReadOnlySwagger Swagger { get; private set; }
}