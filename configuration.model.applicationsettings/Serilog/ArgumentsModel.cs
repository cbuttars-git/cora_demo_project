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

using static Navient.Presentation.Configuration.Model.Constant.Arguments;

namespace Navient.Presentation.Configuration.Model.Serilog;

/// <summary>
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class ArgumentsModel : JsonBase<ArgumentsModel>, IReadOnlyArguments
{
  /// <summary>
  /// </summary>
  [JsonConstructor]
  public ArgumentsModel()
  {
  }

  /// <summary>
  /// </summary>
  /// <param name="configurationSection"></param>
  internal ArgumentsModel(IConfigurationSection configurationSection)
  {
    Buffered = configurationSection.GetValue<bool>(JSON_PROPERTY_NAME_BUFFERED);

    Configure = new List<IReadOnlyConfigure>(
        configurationSection.GetSection(JSON_PROPERTY_NAME_CONFIGURE).GetChildren().Select(configuration => configuration.ConfigureFactory()));
    RollOnFileSizeLimit = configurationSection.GetValue<bool>(JSON_PROPERTY_NAME_ROLL_ON_FILE_SIZE_LIMIT);
    RetainedFileCountLimit = configurationSection.GetValue<int>(JSON_PROPERTY_NAME_RETAINED_FILE_COUNT_LIMIT);
    FileSizeLimitBytes = configurationSection.GetValue<long>(JSON_PROPERTY_NAME_FILE_SIZE_LIMIT_BYTES);
    RollingInterval = configurationSection.GetValue<RollingInterval>(JSON_PROPERTY_NAME_ROLLING_INTERVAL);
    OutputTemplate = configurationSection.GetValue<string>(JSON_PROPERTY_NAME_OUTPUT_TEMPLATE);
    Path = configurationSection.GetValue<string>(JSON_PROPERTY_NAME_PATH);
  }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_BUFFERED)]
  public bool? Buffered { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_ROLL_ON_FILE_SIZE_LIMIT)]
  public bool? RollOnFileSizeLimit { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_RETAINED_FILE_COUNT_LIMIT)]
  public int? RetainedFileCountLimit { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_CONFIGURE)]
  public IReadOnlyList<IReadOnlyConfigure> Configure { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_FILE_SIZE_LIMIT_BYTES)]
  public long? FileSizeLimitBytes { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_ROLLING_INTERVAL)]
  [JsonConverter(typeof(StringEnumConverter))]
  public RollingInterval? RollingInterval { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_OUTPUT_TEMPLATE)]
  public string OutputTemplate { get; private set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_PATH)]
  public string Path { get; private set; }
}