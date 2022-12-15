// 
//    Project:  FindAndReplace
//    Created:  24-2022-10
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

namespace Navient.Presentation.Service.Rest;

/// <summary>
/// </summary>
public static class Constant
{
  /// <summary>
  /// </summary>
  public const string MEDIA_TYPE_APPLICATION_JSON = @"application/json";

  /// <summary>
  /// </summary>
  public static class Application
  {
    /// <summary>
    /// </summary>
    public const string API_VERSIONED_EXPLORER_GROUP_FORMAT = @"'v'VVV";

    /// <summary>
    /// </summary>
    public const string DEFAULT_APPLICATION_NAME = @"Find And Replace";

    /// <summary>
    /// </summary>
    public const string ENVIRONMENT_FILE_EXTENSION = @".env";

    /// <summary>
    /// </summary>
    public const string SWAGGER_RELATIVE_URI = @"swagger";
  }

  /// <summary>
  /// </summary>
  public static class Health
  {
    /// <summary>
    /// </summary>
    public const string ENDPOINT = @"/health";
  }

  /// <summary>
  /// </summary>
  public static class MediaType
  {
    /// <summary>
    /// </summary>
    public const string APPLICATION_JSON = @"application/json";

    /// <summary>
    /// </summary>
    public const string TEXT_JSON = @"text/json";

    /// <summary>
    /// </summary>
    public const string TEXT_PLAIN = @"text/plain";
  }

  /// <summary>
  /// </summary>
  public static class SwaggerAnnotations
  {
    /// <summary>
    /// </summary>
    public static class WattsNController
    {
      /// <summary>
      /// </summary>
      public const string FIND_AND_REPLACE_REQUEST_BODY_DESCRIPTION = @"The find and replace request body.";
    }
  }
}