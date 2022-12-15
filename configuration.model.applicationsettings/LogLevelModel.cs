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

using static Navient.Presentation.Configuration.Model.Constant.LogLevel;

namespace Navient.Presentation.Configuration.Model;

/// <summary>
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class LogLevelModel : JsonBase<LogLevelModel>, IReadOnlyLogLevel
{
  /// <summary>
  /// </summary>
  [JsonConstructor]
  public LogLevelModel()
  {
  }

  /// <summary>
  /// </summary>
  /// <param name="configurationSection"></param>
  internal LogLevelModel(IConfigurationSection configurationSection)
  {
    Default = configurationSection.GetValue<LogLevel?>(JSON_PROPERTY_NAME_DEFAULT);
    Microsoft = configurationSection.GetValue<LogLevel?>(JSON_PROPERTY_NAME_MICROSOFT);
    System = configurationSection.GetValue<LogLevel?>(JSON_PROPERTY_NAME_SYSTEM);
  }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_DEFAULT)]
  [JsonConverter(typeof(StringEnumConverter))]
  public LogLevel? Default { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_MICROSOFT)]
  [JsonConverter(typeof(StringEnumConverter))]
  public LogLevel? Microsoft { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_SYSTEM)]
  [JsonConverter(typeof(StringEnumConverter))]
  public LogLevel? System { get; private set; }
}