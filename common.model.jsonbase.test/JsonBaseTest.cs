// 
//    Project:  model.jsonbase.test
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
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Navient.Test.Common.Model;

/// <summary>
/// </summary>
public class JsonBaseTest
{
  private const string _CLASS_NAME = nameof(JsonBaseTest);
  private const string _OBJECT_UNDER_TEST_NAME = @"JsonBase<T>";
  private const string _TO_JSON_METHOD = @"ToJson";

  /// <summary>
  /// </summary>
  [Fact(DisplayName = $@"{_CLASS_NAME} - {_OBJECT_UNDER_TEST_NAME}: {_TO_JSON_METHOD} Can Deserialize Test")]
  public void JsonBaseToJsonMethodCanDeserializeTest()
  {
    const string _EXPECTED_JSON_STRING = @"{
  ""double_property"": 142.5685,
  ""float_array_property"": [
    12456.99,
    3.4028235E+38,
    555.55554
  ],
  ""inner_json_base_class_property"": {
    ""date_time_property"": ""2020-05-25T22:43:39"",
    ""integer_property"": 99999,
    ""string_property"": ""Oh What a Test""
  },
  ""string_list_property"": [
    ""This"",
    ""is"",
    ""a"",
    ""list""
  ],
  ""ThisIsATest"": ""ThisIsATest""
}";

    var actualTestObject = JsonBaseTestClass.FromJson(_EXPECTED_JSON_STRING);

    Assert.NotNull(actualTestObject);
    Assert.NotNull(actualTestObject.InnerJsonBaseClassProperty);
    Assert.Equal(EXPECTED_DOUBLE_PROPERTY_TYPE_VALUE, actualTestObject.DoubleProperty);
    Assert.Equal(EXPECTED_FLOAT_ARRAY_PROPERTY_VALUE_0, actualTestObject.FloatArrayProperty[0]);
    Assert.Equal(EXPECTED_FLOAT_ARRAY_PROPERTY_VALUE_1, actualTestObject.FloatArrayProperty[1]);
    Assert.Equal(EXPECTED_FLOAT_ARRAY_PROPERTY_VALUE_2, actualTestObject.FloatArrayProperty[2]);
    Assert.Equal(EXPECTED_STRING_LIST_PROPERTY_VALUE_0, actualTestObject.StringListProperty[0]);
    Assert.Equal(EXPECTED_STRING_LIST_PROPERTY_VALUE_1, actualTestObject.StringListProperty[1]);
    Assert.Equal(EXPECTED_STRING_LIST_PROPERTY_VALUE_2, actualTestObject.StringListProperty[2]);
    Assert.Equal(EXPECTED_STRING_LIST_PROPERTY_VALUE_3, actualTestObject.StringListProperty[3]);
    Assert.Equal(EXPECTED_JSON_PROPERTY_TEST_NAME, actualTestObject.JsonPropertyNamedProperty);
    Assert.Equal(ExpectedDateTimeProperty, actualTestObject.InnerJsonBaseClassProperty.DateTimeProperty);
    Assert.Equal(EXPECTED_INTEGER_PROPERTY_VALUE, actualTestObject.InnerJsonBaseClassProperty.IntegerProperty);
    Assert.Equal(EXPECTED_STRING_PROPERTY_VALUE, actualTestObject.InnerJsonBaseClassProperty.StringProperty);
    Assert.Equal(0, actualTestObject.InnerJsonBaseClassProperty.DefaultIntegerProperty);
    Assert.Equal(0, actualTestObject.InnerJsonBaseClassProperty.DefaultDoubleProperty);
    Assert.Equal(DateTime.MinValue, actualTestObject.InnerJsonBaseClassProperty.DefaultDateTimeProperty);

    var actualJsonString = actualTestObject.ToJson(true);
    Assert.NotNull(actualJsonString);
    Assert.NotEmpty(actualJsonString);
    Assert.Equal(_EXPECTED_JSON_STRING, actualJsonString);
  }

  /// <summary>
  /// </summary>
  [Fact(DisplayName = $@"{_CLASS_NAME} - {_OBJECT_UNDER_TEST_NAME}: {_TO_JSON_METHOD} Can Serialize Test")]
  public void JsonBaseToJsonMethodCanSerializeTest()
  {
    var expected = new JsonBaseTestClass
    {
      DoubleProperty = EXPECTED_DOUBLE_PROPERTY_TYPE_VALUE,
      FloatArrayProperty = new[] { EXPECTED_FLOAT_ARRAY_PROPERTY_VALUE_0, EXPECTED_FLOAT_ARRAY_PROPERTY_VALUE_1, EXPECTED_FLOAT_ARRAY_PROPERTY_VALUE_2 },
      InnerJsonBaseClassProperty = new InnerJsonBaseClass(null)
      {
        DateTimeProperty = ExpectedDateTimeProperty,
        IntegerProperty = EXPECTED_INTEGER_PROPERTY_VALUE,
        StringProperty = EXPECTED_STRING_PROPERTY_VALUE
      },
      StringListProperty = new List<string>
            { EXPECTED_STRING_LIST_PROPERTY_VALUE_0, EXPECTED_STRING_LIST_PROPERTY_VALUE_1, EXPECTED_STRING_LIST_PROPERTY_VALUE_2, EXPECTED_STRING_LIST_PROPERTY_VALUE_3 },
      JsonPropertyNamedProperty = EXPECTED_JSON_PROPERTY_TEST_NAME,
      NullStringProperty = null
    };


    var actual = expected.ToJson(true);

    Assert.NotNull(actual);
    Assert.NotEmpty(actual);
    Assert.IsType<string>(actual);


    var actualJsonObject = JObject.Parse(actual);
    Assert.NotNull(actualJsonObject);

    var actualDoublePropertyTypeToken = actualJsonObject.SelectToken(@"double_property");
    Assert.NotNull(actualDoublePropertyTypeToken);
    Assert.Equal(JTokenType.Float, actualDoublePropertyTypeToken.Type);
    Assert.Equal(expected.DoubleProperty, actualDoublePropertyTypeToken.Value<double>());

    var actualFloatArrayPropertyToken = actualJsonObject.SelectToken(@"float_array_property");
    Assert.NotNull(actualFloatArrayPropertyToken);
    Assert.Equal(JTokenType.Array, actualFloatArrayPropertyToken.Type);
    Assert.NotEmpty(actualFloatArrayPropertyToken);
    Assert.Equal(expected.FloatArrayProperty.Length, actualFloatArrayPropertyToken.Count());
    Assert.Contains(expected.FloatArrayProperty,
        item => { return actualFloatArrayPropertyToken.Count(x => x is not null && x.Type.Equals(JTokenType.Float) && x.Value<float>().Equals(item)) == 1; });

    var actualStringListPropertyToken = actualJsonObject.SelectToken(@"string_list_property");
    Assert.NotNull(actualStringListPropertyToken);
    Assert.Equal(JTokenType.Array, actualStringListPropertyToken.Type);
    Assert.NotEmpty(actualStringListPropertyToken);
    Assert.Equal(expected.StringListProperty.Count, actualStringListPropertyToken.Count());
    Assert.Contains(expected.StringListProperty, item =>
    {
      // ReSharper disable once PossibleNullReferenceException 
      // ReSharper is clearly confused here.
      return actualStringListPropertyToken.Count(x => x is not null && x.Type.Equals(JTokenType.String) && x.Value<string>().Equals(item)) == 1;
    });

    var actualJsonPropertyNamedPropertyToken = actualJsonObject.SelectToken(EXPECTED_JSON_PROPERTY_TEST_NAME)?.Value<string>();
    Assert.NotNull(actualJsonPropertyNamedPropertyToken);
    Assert.Equal(expected.JsonPropertyNamedProperty, actualJsonPropertyNamedPropertyToken);

    Assert.Null(actualJsonObject.SelectToken(@"null_string_property"));

    var actualInnerJsonBaseClassPropertyToken = actualJsonObject.SelectToken(@"inner_json_base_class_property");
    Assert.NotNull(actualInnerJsonBaseClassPropertyToken);
    Assert.Equal(JTokenType.Object, actualInnerJsonBaseClassPropertyToken.Type);

    var actualInnerJsonBaseClassDateTimeProperty = actualJsonObject.SelectToken(@"inner_json_base_class_property.date_time_property");
    Assert.NotNull(actualInnerJsonBaseClassDateTimeProperty);
    Assert.Equal(actualInnerJsonBaseClassPropertyToken.SelectToken(@"date_time_property"), actualInnerJsonBaseClassDateTimeProperty);
    Assert.Equal(JTokenType.Date, actualInnerJsonBaseClassDateTimeProperty.Type);
    Assert.Equal(expected.InnerJsonBaseClassProperty.DateTimeProperty, actualInnerJsonBaseClassDateTimeProperty.Value<DateTime>());

    var actualInnerJsonBaseClassDefaultDateTimeProperty = actualJsonObject.SelectToken(@"inner_json_base_class_property.default_date_time_property");
    Assert.Null(actualInnerJsonBaseClassDefaultDateTimeProperty);
    Assert.Null(actualInnerJsonBaseClassPropertyToken.SelectToken(@"default_date_time_property"));
    Assert.Equal(DateTime.MinValue, expected.InnerJsonBaseClassProperty.DefaultDateTimeProperty);

    var actualInnerJsonBaseClassDefaultDoubleProperty = actualJsonObject.SelectToken(@"inner_json_base_class_property.default_double_property");
    Assert.Null(actualInnerJsonBaseClassDefaultDoubleProperty);
    Assert.Null(actualInnerJsonBaseClassPropertyToken.SelectToken(@"default_double_property"));
    Assert.Equal(0, expected.InnerJsonBaseClassProperty.DefaultDoubleProperty);

    var actualInnerJsonBaseClassDefaultIntegerPropertyToken = actualJsonObject.SelectToken(@"inner_json_base_class_property.default_integer_property");
    Assert.Null(actualInnerJsonBaseClassDefaultIntegerPropertyToken);
    Assert.Null(actualInnerJsonBaseClassPropertyToken.SelectToken(@"default_integer_property"));
    Assert.Equal(0, expected.InnerJsonBaseClassProperty.DefaultIntegerProperty);

    var actualInnerJsonBaseClassIntegerProperty = actualJsonObject.SelectToken(@"inner_json_base_class_property.integer_property");
    Assert.NotNull(actualInnerJsonBaseClassIntegerProperty);
    Assert.NotNull(actualInnerJsonBaseClassPropertyToken.SelectToken(@"integer_property"));
    Assert.Equal(JTokenType.Integer, actualInnerJsonBaseClassIntegerProperty.Type);
    Assert.Equal(JTokenType.Integer, actualInnerJsonBaseClassPropertyToken.SelectToken(@"integer_property")?.Type);
    Assert.Equal(actualInnerJsonBaseClassPropertyToken.SelectToken(@"integer_property"), actualInnerJsonBaseClassIntegerProperty);
    Assert.Equal(expected.InnerJsonBaseClassProperty.IntegerProperty, actualInnerJsonBaseClassIntegerProperty.Value<int>());
    Assert.Equal(expected.InnerJsonBaseClassProperty.IntegerProperty, actualInnerJsonBaseClassPropertyToken.SelectToken(@"integer_property")?.Value<int>());

    var actualInnerJsonBaseClassStringPropertyToken = actualJsonObject.SelectToken(@"inner_json_base_class_property.string_property");
    Assert.NotNull(actualInnerJsonBaseClassStringPropertyToken);
    Assert.NotNull(actualInnerJsonBaseClassPropertyToken.SelectToken(@"string_property"));
    Assert.Equal(JTokenType.String, actualInnerJsonBaseClassStringPropertyToken.Type);
    Assert.Equal(JTokenType.String, actualInnerJsonBaseClassPropertyToken.SelectToken(@"string_property")?.Type);
    Assert.Equal(actualInnerJsonBaseClassPropertyToken.SelectToken(@"string_property"), actualInnerJsonBaseClassStringPropertyToken);
    Assert.Equal(expected.InnerJsonBaseClassProperty.StringProperty, actualInnerJsonBaseClassStringPropertyToken.Value<string>());
    Assert.Equal(expected.InnerJsonBaseClassProperty.StringProperty, actualInnerJsonBaseClassPropertyToken.SelectToken(@"string_property")?.Value<string>());
  }
}

/// <summary>
/// </summary>
public class JsonBaseTestClass : JsonBase<JsonBaseTestClass>
{
  /// <summary>
  /// </summary>
  public double DoubleProperty { get; set; }

  /// <summary>
  /// </summary>
  public float[] FloatArrayProperty { get; set; }

  /// <summary>
  /// </summary>
  public InnerJsonBaseClass InnerJsonBaseClassProperty { get; set; }

  /// <summary>
  /// </summary>
  public List<string> StringListProperty { get; set; }

  /// <summary>
  /// </summary>
  [JsonProperty(EXPECTED_JSON_PROPERTY_TEST_NAME)]
  public string JsonPropertyNamedProperty { get; set; }

  /// <summary>
  /// </summary>
  public string NullStringProperty { get; set; }
}

public class InnerJsonBaseClass : JsonBase<InnerJsonBaseClass>
{
  /// <summary>
  /// </summary>
  public InnerJsonBaseClass()
  {
  }

  /// <summary>
  /// </summary>
  /// <param name="jsonSerializerSettings"></param>
  public InnerJsonBaseClass(JsonSerializerSettings jsonSerializerSettings) : base(jsonSerializerSettings)
  {
  }

  /// <summary>
  /// </summary>
  public DateTime DateTimeProperty { get; set; }

  /// <summary>
  /// </summary>
  public DateTime DefaultDateTimeProperty => DateTime.MinValue;

  /// <summary>
  /// </summary>
  public double DefaultDoubleProperty => 0;

  /// <summary>
  /// </summary>
  public int DefaultIntegerProperty => 0;

  /// <summary>
  /// </summary>
  public int IntegerProperty { get; set; }

  /// <summary>
  /// </summary>
  public string StringProperty { get; set; }
}