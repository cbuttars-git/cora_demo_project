// 
//    Project:  Common.Model
//    Created:  26-2022-10
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

using Newtonsoft.Json.Serialization;

namespace Navient.Common.Model;

/// <summary>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class JsonBase<T> : IJsonBase where T : class, IJsonBase, new()
{
  private readonly JsonSerializerSettings _jsonSerializerSettings;

  /// <summary>
  /// </summary>
  /// <param name="jsonSerializerSettings"></param>
  // ReSharper disable once UnusedMember.Global
  protected JsonBase(JsonSerializerSettings jsonSerializerSettings)
  {
    _jsonSerializerSettings = jsonSerializerSettings ?? InitializeJsonSerializerSettings;
  }

  /// <summary>
  /// </summary>
  protected JsonBase()
  {
    _jsonSerializerSettings = InitializeJsonSerializerSettings;
  }

  private static JsonSerializerSettings InitializeJsonSerializerSettings => new()
  {
      NullValueHandling = NullValueHandling.Ignore,
      DefaultValueHandling = DefaultValueHandling.Ignore,
      DateFormatHandling = DateFormatHandling.IsoDateFormat,
      DateParseHandling = DateParseHandling.DateTime,
      DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
      TypeNameHandling = TypeNameHandling.None,
      ContractResolver = new DefaultContractResolver
      {
          NamingStrategy = new SnakeCaseNamingStrategy()
      }
  };

  /// <summary>
  ///   Serializes this object into a JSON string based on the JSON rules set on this object.
  ///   If other JSON setting are needed consider overriding this method.
  /// </summary>
  /// <param name="prettyPrint">Default Value: false</param>
  /// <returns>string</returns>
  public virtual string ToJson(bool prettyPrint = false)
  {
    return JsonConvert.SerializeObject(this, prettyPrint ? Formatting.Indented : Formatting.None, _jsonSerializerSettings);
  }

  /// <summary>
  ///   Accepts a JSON string, instantiates an instance of this object and attempts to hydrate the object with values from the JSON string based on the JSON rules set on this object.
  /// </summary>
  /// <param name="jsonString"></param>
  /// <param name="jsonSerializerSettings"></param>
  /// <returns>class object of type T.</returns>
  public static T FromJson(string jsonString, JsonSerializerSettings jsonSerializerSettings = null)
  {
    return JsonConvert.DeserializeObject<T>(jsonString, jsonSerializerSettings ?? InitializeJsonSerializerSettings);
  }
}