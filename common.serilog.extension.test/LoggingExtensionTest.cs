// 
//    Project:  Common.Serilog.Extension.Test
//    Created:  25-2022-11
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
using static Navient.Common.Extension.Constant;

namespace Navient.Test.Common.Extension;

public class LoggingExtensionTest
{
  private const string _CLASS_NAME = $@"{nameof(LoggingExtensionTest)}:";

  //[InlineData(LogEventLevel.Debug)]
  //[InlineData(LogEventLevel.Verbose)]
  //[Theory(DisplayName = $@"{_CLASS_NAME} Extension Method {nameof(LoggingExtension.LogApplicationSettingsAreValid)} Can Write Test")]
  //public void LogApplicationSettingsAreValidCanWriteTest(LogEventLevel configuredLogEventLevel)
  //{
  //  const LogEventLevel _EXPECTED_LOG_EVENT_LEVEL = LogEventLevel.Debug;
  //  var fakeLogger = A.Fake<ILogger>();
  //  Log.Logger = ConfigureLoggerLogEventLevel(configuredLogEventLevel);
  //  var testDelegate = new Func<bool>(() => Log.Logger.IsEnabled(_EXPECTED_LOG_EVENT_LEVEL));

  //  A.CallTo(() => fakeLogger.IsEnabled(A<LogEventLevel>._)).Returns(testDelegate());
  //  var actual = fakeLogger.LogApplicationSettingsAreValid();

  //  Assert.NotNull(actual);
  //  Assert.Equal(fakeLogger, actual);
  //  A.CallTo(() => fakeLogger.IsEnabled(_EXPECTED_LOG_EVENT_LEVEL)).MustHaveHappenedOnceExactly();
  //  A.CallTo(() => fakeLogger.Write(_EXPECTED_LOG_EVENT_LEVEL, LOGGING_MESSAGE_APPSETTINGS_VALID)).MustHaveHappenedOnceExactly();

  //  Log.CloseAndFlush();
  //}

  //[InlineData(LogEventLevel.Error)]
  //[InlineData(LogEventLevel.Fatal)]
  //[InlineData(LogEventLevel.Warning)]
  //[InlineData(LogEventLevel.Information)]
  //[Theory(DisplayName = $@"{_CLASS_NAME} Extension Method {nameof(LoggingExtension.LogApplicationSettingsAreValid)} Does Not Write Test")]
  //public void LogApplicationSettingsAreValidDoesNotWrite(LogEventLevel configuredLogEventLevel)
  //{
  //  const LogEventLevel _EXPECTED_LOG_EVENT_LEVEL = LogEventLevel.Debug;
  //  var fakeLogger = A.Fake<ILogger>();
  //  Log.Logger = ConfigureLoggerLogEventLevel(configuredLogEventLevel);
  //  var testDelegate = new Func<bool>(() => Log.Logger.IsEnabled(_EXPECTED_LOG_EVENT_LEVEL));
  //  A.CallTo(() => fakeLogger.IsEnabled(A<LogEventLevel>._)).Returns(testDelegate());
  //  var actual = fakeLogger.LogApplicationSettingsAreValid();

  //  Assert.NotNull(actual);
  //  Assert.Equal(fakeLogger, actual);
  //  A.CallTo(() => fakeLogger.IsEnabled(_EXPECTED_LOG_EVENT_LEVEL)).MustHaveHappenedOnceExactly();
  //  A.CallTo(() => fakeLogger.Write(A<LogEventLevel>._, LOGGING_MESSAGE_APPSETTINGS_VALID)).MustNotHaveHappened();

  //  Log.CloseAndFlush();
  //}

  [InlineData(LogEventLevel.Debug)]
  [InlineData(LogEventLevel.Verbose)]
  [InlineData(LogEventLevel.Information)]
  [Theory(DisplayName = $@"{_CLASS_NAME} Extension Method {nameof(LoggingExtension.LogStartUpMessage)} Can Write Test")]
  public void LogStartUpMessageCanWriteTest(LogEventLevel configuredLogEventLevel)
  {
    const LogEventLevel _EXPECTED_LOG_EVENT_LEVEL = LogEventLevel.Information;
    const string _APPLICATION_NAME = @"Test Application Name";
    var fakeLogger = A.Fake<ILogger>();
    Log.Logger = ConfigureLoggerLogEventLevel(configuredLogEventLevel);
    var testDelegate = new Func<bool>(() => Log.Logger.IsEnabled(_EXPECTED_LOG_EVENT_LEVEL));

    A.CallTo(() => fakeLogger.IsEnabled(A<LogEventLevel>._)).Returns(testDelegate());
    var actual = fakeLogger.LogStartUpMessage(_APPLICATION_NAME);

    Assert.NotNull(actual);
    Assert.Equal(fakeLogger, actual);
    A.CallTo(() => fakeLogger.IsEnabled(_EXPECTED_LOG_EVENT_LEVEL)).MustHaveHappenedOnceExactly();
    A.CallTo(() => fakeLogger.Write(_EXPECTED_LOG_EVENT_LEVEL, LOGGING_MESSAGE_APPLICATION_START, _APPLICATION_NAME)).MustHaveHappenedOnceExactly();

    Log.CloseAndFlush();
  }

  [InlineData(LogEventLevel.Error)]
  [InlineData(LogEventLevel.Fatal)]
  [InlineData(LogEventLevel.Warning)]
  [Theory(DisplayName = $@"{_CLASS_NAME} Extension Method {nameof(LoggingExtension.LogStartUpMessage)} Does Not Write Test")]
  public void LogStartUpMessageDoesNotWrite(LogEventLevel configuredLogEventLevel)
  {
    const LogEventLevel _EXPECTED_LOG_EVENT_LEVEL = LogEventLevel.Information;
    const string _APPLICATION_NAME = @"Test Application Name";
    var fakeLogger = A.Fake<ILogger>();
    Log.Logger = ConfigureLoggerLogEventLevel(configuredLogEventLevel);
    var testDelegate = new Func<bool>(() => Log.Logger.IsEnabled(_EXPECTED_LOG_EVENT_LEVEL));
    A.CallTo(() => fakeLogger.IsEnabled(A<LogEventLevel>._)).Returns(testDelegate());
    var actual = fakeLogger.LogStartUpMessage(_APPLICATION_NAME);

    Assert.NotNull(actual);
    Assert.Equal(fakeLogger, actual);
    A.CallTo(() => fakeLogger.IsEnabled(_EXPECTED_LOG_EVENT_LEVEL)).MustHaveHappenedOnceExactly();
    A.CallTo(() => fakeLogger.Write(A<LogEventLevel>._, LOGGING_MESSAGE_APPLICATION_START, A<string>._)).MustNotHaveHappened();

    Log.CloseAndFlush();
  }

  private static ILogger ConfigureLoggerLogEventLevel(LogEventLevel logEventLevel)
  {
    return new LoggerConfiguration()
        .MinimumLevel
        .Is(logEventLevel)
        .CreateLogger();
  }
}