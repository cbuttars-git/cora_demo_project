// 
//    Project:  extension.json
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

namespace Navient.Common.Extension;

/// <summary>
/// </summary>
public static class Extension
{
  /// <summary>
  /// </summary>
  /// <param name="environment"></param>
  /// <returns></returns>
  public static Func<JsonSerializerSettings> AddDefaultJsonSettings(this IHostEnvironment environment)
  {
    return () =>
    {
      var settings = new JsonSerializerSettings
      {
        ContractResolver = new DefaultContractResolver
        {
          NamingStrategy = new SnakeCaseNamingStrategy()
        },
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Include,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        Formatting = environment.IsDevelopment() ? Formatting.Indented : Formatting.None,
        DateFormatHandling = DateFormatHandling.IsoDateFormat,
        DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
        TypeNameHandling = TypeNameHandling.None
      };
      settings.Converters.Add(new StringEnumConverter(new SnakeCaseNamingStrategy()));
      return settings;
    };
  }
}