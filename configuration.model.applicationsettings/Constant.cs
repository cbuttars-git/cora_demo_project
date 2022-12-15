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

namespace Navient.Presentation.Configuration.Model;

/// <summary>
/// </summary>
public static class Constant
{
  /// <summary>
  /// </summary>
  public static class Api
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_ASSEMBLY_VERSION = @"1.0.0.0";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_DEPRECATION_MESSAGE = @"DepricatedMessage";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_ASSEMBLY_VERSION = @"AssemblyVersion";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_CONTACT = @"Contact";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_DESCRIPTION = @"Description";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_LATEST_API_VERSION = @"LatestApiVersion";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_TITLE = @"Title";
  }

  /// <summary>
  /// </summary>
  public static class Application
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_API = @"Api";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_SWAGGER = @"Swagger";
  }

  /// <summary>
  /// </summary>
  public static class ApplicationSettings
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_ALLOWED_HOSTS = @"AllowedHosts";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_APPLICATION = @"Application";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_CONNECTION_STRINGS = @"ConnectionStrings";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_LOGGING = @"Logging";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_SERILOG = @"Serilog";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_URLS = @"Urls";
  }

  /// <summary>
  /// </summary>
  public static class Arguments
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_BUFFERED = @"buffered";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_CONFIGURE = @"configure";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_FILE_SIZE_LIMIT_BYTES = @"fileSizeLimitBytes";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_OUTPUT_TEMPLATE = @"outputTemplate";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_PATH = @"path";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_RETAINED_FILE_COUNT_LIMIT = @"retainedFileCountLimit";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_ROLL_ON_FILE_SIZE_LIMIT = @"rollOnFileSizeLimit";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_ROLLING_INTERVAL = @"rollingInterval";
  }

  /// <summary>
  /// </summary>
  public static class ConfigurationBase
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_ARGS = @"Args";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_NAME = @"Name";
  }

  /// <summary>
  /// </summary>
  public static class ConnectionString
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_NAME = ConfigurationBase.JSON_PROPERTY_NAME_NAME;

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_VALUE = @"Value";
  }

  /// <summary>
  /// </summary>
  public static class Contact
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_EMAIL = @"Email";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_NAME = ConfigurationBase.JSON_PROPERTY_NAME_NAME;

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_URL = @"Url";
  }

  /// <summary>
  /// </summary>
  public static class Logging
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_LOG_LEVEL = @"LogLevel";
  }

  /// <summary>
  /// </summary>
  public static class LogLevel
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_DEFAULT = @"Default";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_MICROSOFT = Override.JSON_PROPERTY_NAME_MICROSOFT;

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_SYSTEM = @"System";
  }

  /// <summary>
  /// </summary>
  public static class MinimumLevel
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_DEFAULT = @"Default";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_OVERRIDE = @"Override";
  }

  /// <summary>
  /// </summary>
  public static class Override
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_MICROSOFT = @"Microsoft";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_MICROSOFT_HOSTING = @"Microsoft.Hosting";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_SERILOG_ASP_NET_CORE = @"Serilog.AspNetCore";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_SYSTEM = @"System";
  }

  /// <summary>
  /// </summary>
  public static class Serilog
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_ENRICH = @"Enrich";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_MINIMUM_LEVEL = @"MinimumLevel";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_WRITE_TO = @"WriteTo";
  }

  /// <summary>
  /// </summary>
  public static class Swagger
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_IS_ENABLED = @"IsEnabled";

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_USE_ANNOTATIONS = @"UseAnnotations";
  }

  /// <summary>
  /// </summary>
  public static class WriteTo
  {
    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_ARGS = ConfigurationBase.JSON_PROPERTY_NAME_ARGS;

    /// <summary>
    /// </summary>
    public const string JSON_PROPERTY_NAME_NAME = ConfigurationBase.JSON_PROPERTY_NAME_NAME;
  }
}