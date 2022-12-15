// 
//    Project:  Model.ApplicationSettings.Test
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

using Navient.Common.Model;
using Navient.Presentation.Configuration.Model.Serilog;
using Newtonsoft.Json;
using static Navient.Presentation.Configuration.Model.Constant.Arguments;
using static Navient.Test.Configuration.Model.Constant.ArgumentModelTest;

namespace Navient.Test.Configuration.Model.Serilog;

public class ArgumentsModelTest
{
  private const string _CLASS_NAME = nameof(ArgumentsModelTest);
  private const string _NAME_OF_OBJECT_UNDER_TEST = nameof(ArgumentsModel);


  [Fact(DisplayName = $@"{_CLASS_NAME} Class: {_NAME_OF_OBJECT_UNDER_TEST} Can Deserialize Json.")]
  public void ArgumentsModelCanDeserializeJson()
  {
    const string _EXPECTED_JSON_STRING =
        @"{""buffered"":false,""rollOnFileSizeLimit"":true,""retainedFileCountLimit"":10,""fileSizeLimitBytes"":10485760,""rollingInterval"":""Day"",""outputTemplate"":""{Timestamp:o} [{Level:u3}] ({SourceContext}|{Application}|{MachineName}|{ThreadId}|{ThreadName}) {Message}{NewLine}{Exception}"",""path"":""APPLICATION_LOGS/log_.txt""}";

    var actualObjectUnderTest = ArgumentsModel.FromJson(_EXPECTED_JSON_STRING);

    Assert.NotNull(actualObjectUnderTest);

    var expectedJsonObject = JObject.Parse(_EXPECTED_JSON_STRING, new JsonLoadSettings());

    var expectedPathValue = expectedJsonObject.SelectToken(JSON_PROPERTY_NAME_PATH)?.Value<string>();
    Assert.NotNull(actualObjectUnderTest.Path);
    Assert.NotEmpty(actualObjectUnderTest.Path);
    Assert.Equal(expectedPathValue, actualObjectUnderTest.Path);

    var expectedRollingIntervalValue = expectedJsonObject.SelectToken(JSON_PROPERTY_NAME_ROLLING_INTERVAL)?.Value<string>();
    Assert.NotNull(actualObjectUnderTest.RollingInterval);
    Assert.Equal(expectedRollingIntervalValue, actualObjectUnderTest.RollingInterval.ToString());

    var expectedRollOnFileSizeLimit = expectedJsonObject.SelectToken(JSON_PROPERTY_NAME_ROLL_ON_FILE_SIZE_LIMIT)?.Value<bool?>();
    Assert.NotNull(actualObjectUnderTest.RollOnFileSizeLimit);
    Assert.Equal(expectedRollOnFileSizeLimit, actualObjectUnderTest.RollOnFileSizeLimit);

    var expectedFileSizeLimitBytes = expectedJsonObject.SelectToken(JSON_PROPERTY_NAME_FILE_SIZE_LIMIT_BYTES)?.Value<long?>();
    Assert.NotNull(actualObjectUnderTest.FileSizeLimitBytes);
    Assert.Equal(expectedFileSizeLimitBytes, actualObjectUnderTest.FileSizeLimitBytes);

    var expectedRetainedFileCountLimit = expectedJsonObject.SelectToken(JSON_PROPERTY_NAME_RETAINED_FILE_COUNT_LIMIT)?.Value<int?>();
    Assert.NotNull(actualObjectUnderTest.RetainedFileCountLimit);
    Assert.Equal(expectedRetainedFileCountLimit, actualObjectUnderTest.RetainedFileCountLimit);

    var expectedBuffered = expectedJsonObject.SelectToken(JSON_PROPERTY_NAME_BUFFERED)?.Value<bool?>();
    Assert.NotNull(actualObjectUnderTest.Buffered);
    Assert.Equal(expectedBuffered, actualObjectUnderTest.Buffered);

    var expectedOutputTemplate = expectedJsonObject.SelectToken(JSON_PROPERTY_NAME_OUTPUT_TEMPLATE)?.Value<string>();
    Assert.NotNull(actualObjectUnderTest.OutputTemplate);
    Assert.NotEmpty(actualObjectUnderTest.OutputTemplate);
    Assert.Equal(expectedOutputTemplate, actualObjectUnderTest.OutputTemplate);

    var actualJsonString = actualObjectUnderTest.ToJson();
    Assert.NotNull(actualJsonString);
    Assert.NotEmpty(actualJsonString);
    Assert.Equal(_EXPECTED_JSON_STRING, actualJsonString);
  }

  [Fact(DisplayName = $@"{_CLASS_NAME} Class: {_NAME_OF_OBJECT_UNDER_TEST} Object Validation Test.")]
  public void ArgumentsModelContractTest()
  {
    const int _EXPECTED_PROPERTY_COUNT = 7;
    var actualObjectUnderTest = new ArgumentsModel();

    Assert.NotNull(actualObjectUnderTest);
    Assert.IsAssignableFrom<IReadOnlyArguments>(actualObjectUnderTest);
    Assert.IsAssignableFrom<JsonBase<ArgumentsModel>>(actualObjectUnderTest);
    Assert.IsAssignableFrom<IJsonBase>(actualObjectUnderTest);

    var requiredProperties = typeof(ArgumentsModel).PropertiesFromTypeWithCustomAttribute<JsonPropertyAttribute>();
    Assert.Equal(_EXPECTED_PROPERTY_COUNT, requiredProperties.Count);

    var actualProperty = requiredProperties.ActualPropertyByNameAndAttributeType<JsonPropertyAttribute>(EXPECTED_PROPERTY_BUFFERED, JSON_PROPERTY_NAME_BUFFERED);
    Assert.NotNull(actualProperty);
    Assert.Equal(typeof(bool?), actualProperty.PropertyType);

    actualProperty = requiredProperties.ActualPropertyByNameAndAttributeType<JsonPropertyAttribute>(EXPECTED_PROPERTY_ROLL_ON_FILE_SIZE_LIMIT,
        JSON_PROPERTY_NAME_ROLL_ON_FILE_SIZE_LIMIT);
    Assert.NotNull(actualProperty);
    Assert.Equal(typeof(bool?), actualProperty.PropertyType);

    actualProperty = requiredProperties.ActualPropertyByNameAndAttributeType<JsonPropertyAttribute>(EXPECTED_PROPERTY_RETAINED_FILE_COUNT_LIMIT,
        JSON_PROPERTY_NAME_RETAINED_FILE_COUNT_LIMIT);
    Assert.NotNull(actualProperty);
    Assert.Equal(typeof(int?), actualProperty.PropertyType);
  }
}