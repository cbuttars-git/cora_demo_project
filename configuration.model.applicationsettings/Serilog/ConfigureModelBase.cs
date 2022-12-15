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

using static Navient.Presentation.Configuration.Model.Constant.ConfigurationBase;

namespace Navient.Presentation.Configuration.Model.Serilog;

/// <summary>
/// </summary>
public abstract class ConfigureModelBase<T> : JsonBase<T>, IReadOnlyConfigure where T : class, IReadOnlyConfigure, IJsonBase, new()
{
  /// <summary>
  /// </summary>
  protected ConfigureModelBase()
  {
  }

  /// <summary>
  /// </summary>
  /// <param name="configurationSection"></param>
  protected ConfigureModelBase(IConfigurationSection configurationSection)
  {
    // ReSharper disable twice VirtualMemberCallInConstructor
    // It is a base class. If overriding; just use the default constructor.
    Arguments = configurationSection.GetSection(JSON_PROPERTY_NAME_ARGS).ArgumentsFactory();
    Name = configurationSection.GetValue<string>(JSON_PROPERTY_NAME_NAME);
  }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_ARGS)]
  public virtual IReadOnlyArguments Arguments { get; protected set; }

  /// <summary>
  /// </summary>
  [JsonProperty(JSON_PROPERTY_NAME_NAME)]
  public virtual string Name { get; protected set; }
}