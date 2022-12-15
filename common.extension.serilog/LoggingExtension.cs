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

using System;
using System.Diagnostics;
using System.Net;
using Serilog.Events;

namespace Navient.Common.Extension;

// ReSharper disable once UnusedMember.Global
// Class is used to contain extension methods and may never actually have a reference to the class name.
/// <summary>
/// </summary>
public static class LoggingExtension
{
  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static ILogger LogApplicationBrowsableUri(this ILogger parameter, Uri uri)
  {
    if (parameter.IsEnabled(LogEventLevel.Information))
      parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_APPLICATION_BROWSER_URL, uri);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <returns></returns>
  public static ILogger LogApplicationConfigurationComplete(this ILogger parameter)
  {
    if (parameter.IsEnabled(LogEventLevel.Information))
      parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_APPLICATION_CONFIGURATION_COMPLETE);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="className"></param>
  /// <param name="methodName"></param>
  /// <returns></returns>
  public static ILogger LogApplicationSettingAddedAsAService(this ILogger parameter, string className, string methodName)
  {
    if (parameter.IsEnabled(LogEventLevel.Information))
      parameter.Write(LogEventLevel.Debug, LOGGING_MESSAGE_APPLICATION_SETTINGS_READY_FOR_DEPENDENCY_INJECTION, className, methodName);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="applicationSettingsSerializationDelegate"></param>
  /// <returns></returns>
  public static ILogger LogApplicationSettings(this ILogger parameter, Func<string> applicationSettingsSerializationDelegate)
  {
    if (parameter.IsEnabled(LogEventLevel.Information) && applicationSettingsSerializationDelegate is not null)
      parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_APPLICATION_SETTINGS_JSON, applicationSettingsSerializationDelegate());
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="webHostCreatedName"></param>
  /// <returns></returns>
  public static ILogger LogApplicationWebHostCreatedMessage(this ILogger parameter, string webHostCreatedName)
  {
    if (parameter.IsEnabled(LogEventLevel.Information))
      parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_WEB_HOST_CONTEXT_CREATED, webHostCreatedName);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <returns></returns>
  public static ILogger LogDevelopmentExceptionPageLoaded(this ILogger parameter)
  {
    if (parameter.IsEnabled(LogEventLevel.Information))
      parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_DEVELOPMENT_EXCEPTION_PAGE);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="className"></param>
  /// <returns></returns>
  public static ILogger LogEnteringConstructor(this ILogger parameter, string className)
  {
    if (parameter.IsEnabled(LogEventLevel.Debug))
      parameter.Write(LogEventLevel.Debug, LOGGING_MESSAGE_ENTERING_CONSTRUCTOR, className);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="className"></param>
  /// <param name="methodName"></param>
  /// <returns></returns>
  public static ILogger LogEnteringMethod(this ILogger parameter, string className, string methodName)
  {
    if (parameter.IsEnabled(LogEventLevel.Debug))
      parameter.Write(LogEventLevel.Debug, LOGGING_MESSAGE_ENTERING_METHOD, className, methodName);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="className"></param>
  /// <param name="methodName"></param>
  /// <param name="exception"></param>
  /// <returns></returns>
  public static ILogger LogErrorExceptionHandled(this ILogger parameter, string className, string methodName, Exception exception)
  {
    if (parameter.IsEnabled(LogEventLevel.Error))
      parameter.Write(LogEventLevel.Error, exception, LOGGING_MESSAGE_ERROR_EXCEPTION_MESSAGE, className, methodName);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="className"></param>
  /// <param name="methodName"></param>
  /// <param name="exception"></param>
  /// <returns></returns>
  public static ILogger LogFatalException(this ILogger parameter, string className, string methodName, Exception exception)
  {
    parameter.Write(LogEventLevel.Fatal, exception, LOGGING_MESSAGE_FATAL_EXCEPTION_MESSAGE, className, methodName);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="httpStatusCode"></param>
  /// <param name="exception"></param>
  /// <returns></returns>
  public static ILogger LogGlobalExceptionHandled(this ILogger parameter, HttpStatusCode httpStatusCode, Exception exception)
  {
    if (parameter.IsEnabled(LogEventLevel.Error))
      parameter.Write(LogEventLevel.Error, exception, LOGGING_MESSAGE_GLOBAL_EXCEPTION, httpStatusCode);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <returns></returns>
  public static ILogger LogGlobalExceptionHandlerInUse(this ILogger parameter)
  {
    if (parameter.IsEnabled(LogEventLevel.Information))
      parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_GLOBAL_EXCEPTION_HANDLER);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="className"></param>
  /// <returns></returns>
  public static ILogger LogLeavingConstructor(this ILogger parameter, string className)
  {
    if (parameter.IsEnabled(LogEventLevel.Debug))
      parameter.Write(LogEventLevel.Debug, LOGGING_MESSAGE_LEAVING_CONSTRUCTOR, className);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="className"></param>
  /// <param name="methodName"></param>
  /// <returns></returns>
  public static ILogger LogLeavingMethod(this ILogger parameter, string className, string methodName)
  {
    if (parameter.IsEnabled(LogEventLevel.Debug))
      parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_LEAVING_METHOD, className, methodName);

    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <returns></returns>
  public static ILogger LogSerilogIsStarting(this ILogger parameter)
  {
    if (parameter.IsEnabled(LogEventLevel.Information))
      parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_SERILOG_STARTED_WITH_CONFIGURATION);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="applicationName"></param>
  /// <returns></returns>
  public static ILogger LogStartUpMessage(this ILogger parameter, string applicationName)
  {
    if (parameter.IsEnabled(LogEventLevel.Information))
      parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_APPLICATION_START, applicationName);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="className"></param>
  /// <param name="methodName"></param>
  /// <param name="exception"></param>
  /// <returns></returns>
  public static ILogger LogWarningException(this ILogger parameter, string className, string methodName, Exception exception)
  {
    parameter.Write(LogEventLevel.Warning, exception, LOGGING_MESSAGE_WARNING_EXCEPTION_MESSAGE, className, methodName);
    return parameter;
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="className"></param>
  /// <param name="methodName"></param>
  /// <param name="stopWatchDelegate"></param>
  /// <returns></returns>
  public static Tuple<ILogger, Stopwatch> LogEnteringMethod(this ILogger parameter, string className, string methodName, Func<Stopwatch> stopWatchDelegate)
  {
    Stopwatch result;
    if (!parameter.IsEnabled(LogEventLevel.Information)) return new Tuple<ILogger, Stopwatch>(parameter, stopWatchDelegate?.Invoke());
    parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_ENTERING_METHOD_WITH_STOPWATCH, className, methodName, result = stopWatchDelegate?.Invoke());

    return new Tuple<ILogger, Stopwatch>(parameter, result);
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="className"></param>
  /// <param name="methodName"></param>
  /// <param name="stopWatch"></param>
  /// <returns></returns>
  public static Tuple<ILogger, Stopwatch> LogLeavingMethod(this ILogger parameter, string className, string methodName, Stopwatch stopWatch)
  {
    if (!parameter.IsEnabled(LogEventLevel.Information)) return new Tuple<ILogger, Stopwatch>(parameter, stopWatch);
    Func<TimeSpan> stopWatchDelegate = null;

    if (stopWatch is not null)
      stopWatchDelegate = () =>
      {
        stopWatch.Stop();
        return stopWatch.Elapsed;
      };

    parameter.Write(LogEventLevel.Information, LOGGING_MESSAGE_LEAVING_METHOD_WITH_STOPWATCH, className, methodName, stopWatchDelegate?.Invoke());

    return new Tuple<ILogger, Stopwatch>(parameter, stopWatch);
  }

  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <param name="applicationName"></param>
  public static void LogApplicationIsShuttingDown(this ILogger parameter, string applicationName)
  {
    if (parameter.IsEnabled(LogEventLevel.Warning))
      parameter.Write(LogEventLevel.Warning, LOG_MESSAGE_APPLICATION_STOP, applicationName);
  }
}