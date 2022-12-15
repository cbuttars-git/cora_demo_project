// 
//    Project:  Model.ApplicationSettings.Test
//    Created:  28-2022-10
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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Navient.Test.Configuration.Model;

public static class Extension
{
  public static IList<PropertyInfo> PropertiesFromType(this Type parameter)
  {
    return parameter.GetProperties();
  }

  public static IList<PropertyInfo> PropertiesFromTypeWithCustomAttribute<T>(this Type parameter) where T : Attribute
  {
    var properties = parameter.PropertiesFromType();
    if (properties?.Any() is not true) return null;
    return properties
        .Where(property => property.GetCustomAttribute<JsonPropertyAttribute>() is not null)
        .ToList();
  }

  public static PropertyInfo ActualPropertyByNameAndAttributeType<T>(this IList<PropertyInfo> parameter, string propertyName, string jsonPropertyName) where T : Attribute
  {
    if (parameter?.Any() is not true) return null;
    return parameter.FirstOrDefault(property =>
        property.CanRead && property.CanWrite && property.Name.Equals(propertyName) && property.GetCustomAttribute<T>() is not null &&
        property.GetCustomAttribute<JsonPropertyAttribute>()!.PropertyName!.Equals(jsonPropertyName));
  }
}