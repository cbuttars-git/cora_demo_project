// 
//    Project:  extension.serilog
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
public static class Constant
{
  /// <summary>
  /// </summary>
  public const string LOG_MESSAGE_APPLICATION_STOP = @"{Application} is shutting down!";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_APPLICATION_BROWSER_URL = @"Open {Uri} to browse the server Api.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_APPLICATION_CONFIGURATION_COMPLETE = @"Application configuration is complete.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_APPLICATION_SETTINGS_JSON = @"Merged Application Settings: {ApplicationSettings}";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_APPLICATION_SETTINGS_NOT_VALID = @"AppSetting Section is not valid.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_APPLICATION_SETTINGS_READY_FOR_DEPENDENCY_INJECTION =
      @"{Class}.{Method} has loaded the application settings and they are now ready for Dependency Injection.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_APPLICATION_START = @"{Application} is Starting Up!";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_DEVELOPMENT_EXCEPTION_PAGE = @"Developer exception page loaded.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_ENTERING_CONSTRUCTOR = @"Entering Constructor for {Class}.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_ENTERING_METHOD = @"Entering {Class}.{Method}.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_ENTERING_METHOD_WITH_STOPWATCH = @"Entering {Class}.{Method} with {StopWatch}.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_ERROR_EXCEPTION_MESSAGE = @"Error Exception Caught at Class: {Class} Method: {Method}!";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_FATAL_EXCEPTION_MESSAGE = @"Fatal Exception Caught at Class: {Class} Method: {Method}!";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_GLOBAL_EXCEPTION = @"GLOBAL ERROR HANDLER::HTTP:{HTTPStatusCode}";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_GLOBAL_EXCEPTION_HANDLER = @"Non-Development Environment. Global Exception Handling is being Configured.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_LEAVING_CONSTRUCTOR = @"Leaving Constructor for {Class}.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_LEAVING_METHOD = @"Leaving {Class}.{Method}.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_LEAVING_METHOD_WITH_STOPWATCH = @"Leaving {Class}.{Method} with {StopWatch}.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_SERILOG_STARTED_WITH_CONFIGURATION = @"Serlog has started.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_STARTUP_ENVIRONMENT = @"Start Up {Environment}.";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_WARNING_EXCEPTION_MESSAGE = @"Warning Exception Caught at Class: {Class} Method: {Method}!";

  /// <summary>
  /// </summary>
  public const string LOGGING_MESSAGE_WEB_HOST_CONTEXT_CREATED = @"{ApplicationWebHost} has been Created.";
}