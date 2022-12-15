// 
//    Project:  Common.Model.WattsN
//    Created:  31-2022-10
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

namespace Navient.Common.Model;

/// <summary>
/// </summary>
public static class Constant
{
  /// <summary>
  /// </summary>
  public const string JSON_PROPERTY_NAME_DATA = @"data";

  /// <summary>
  /// </summary>
  public const string JSON_PROPERTY_NAME_MESSAGE = @"message";


  /// <summary>
  /// </summary>
  public static class DataPropertyAnnotation
  {
    /// <summary>
    /// </summary>
    public const string SWAGGER_ANNOTATION_DESCRIPTION =
        @"Reguired JSON text/string that contains the value(s) to replace withing the message. JSON text/string may contain additional keys and values beyond those needed to properly format the return message.";

    /// <summary>
    /// </summary>
    public const string SWAGGER_ANNOTATION_FORMAT = @"text/json";

    /// <summary>
    /// </summary>
    public const string SWAGGER_ANNOTATION_TITLE = @"JSON Data Property";
  }

  /// <summary>
  /// </summary>
  public static class MessagePropertyAnnotation
  {
    /// <summary>
    /// </summary>
    public const string SWAGGER_ANNOTATION_DESCRIPTION =
        @"Message containing variables matching JSON node keys that are in the JSON text/string value of the ""data"" property included in the request payload. Varaibles must be bracketed between tilde (~) charachters. Example: ~json_data.some.value~";

    /// <summary>
    /// </summary>
    public const string SWAGGER_ANNOTATION_FORMAT = @"text/plain";

    /// <summary>
    /// </summary>
    public const string SWAGGER_ANNOTATION_TITLE = @"Message";
  }
}