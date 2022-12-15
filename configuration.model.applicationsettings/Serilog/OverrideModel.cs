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

using static Navient.Presentation.Configuration.Model.Constant.Override;

namespace Navient.Presentation.Configuration.Model.Serilog;

/// <summary>
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class OverrideModel : JsonBase<OverrideModel>, IReadOnlyOverride
{
  /// <summary>
  /// </summary>
  [JsonConstructor]
  public OverrideModel()
  {
  }

  /// <summary>
  /// </summary>
  /// <param name="configurationSection"></param>
  public OverrideModel(IConfigurationSection configurationSection)
  {
    Microsoft = configurationSection.GetValue<LogEventLevel?>(JSON_PROPERTY_NAME_MICROSOFT);
    MicrosoftHosting = configurationSection.GetValue<LogEventLevel?>(JSON_PROPERTY_NAME_MICROSOFT_HOSTING);
    SerilogAspNetCore = configurationSection.GetValue<LogEventLevel?>(JSON_PROPERTY_NAME_SERILOG_ASP_NET_CORE);
    System = configurationSection.GetValue<LogEventLevel?>(JSON_PROPERTY_NAME_SYSTEM);
  }


  /// <summary>
  /// </summary>
  [JsonProperty]
  [JsonConverter(typeof(StringEnumConverter))]
  public LogEventLevel? Microsoft { get; private set; }

  /// <summary>
  /// </summary>
  [JsonConverter(typeof(StringEnumConverter))]
  [JsonProperty(JSON_PROPERTY_NAME_MICROSOFT_HOSTING)]
  public LogEventLevel? MicrosoftHosting { get; private set; }

  /// <summary>
  /// </summary>
  [JsonConverter(typeof(StringEnumConverter))]
  [JsonProperty(JSON_PROPERTY_NAME_SERILOG_ASP_NET_CORE)]
  public LogEventLevel? SerilogAspNetCore { get; private set; }

  /// <summary>
  /// </summary>
  [JsonConverter(typeof(StringEnumConverter))]
  [JsonProperty]
  public LogEventLevel? System { get; private set; }
}