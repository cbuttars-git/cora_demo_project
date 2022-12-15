// 
//    Project:  extension.validation.test
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

namespace Navient.Test.Common.Extension;

/// <summary>
/// </summary>
public class ExtensionTest
{
  private const string _ARGUMENT_EXCEPTION_NAME = nameof(ArgumentNullException);
  private const string _ARGUMENT_NULL_EXCEPTION_NAME = nameof(ArgumentNullException);
  private const string _CLASS_NAME = nameof(ExtensionTest);
  private const string _METHOD_NOT_NULL_OR_THROW_ARGUMENT_NULL_EXCEPTION_NAME = nameof(ext.NotNullOrThrowArgumentNullException);
  private const string _METHOD_VALID_OR_THROW_ARGUMENT_EXCEPTION_NAME = nameof(ext.ValidOrThrowArgumentException);

  /// <summary>
  /// </summary>
  [Fact(DisplayName =
      $@"{_CLASS_NAME} - Extension Method: {_METHOD_NOT_NULL_OR_THROW_ARGUMENT_NULL_EXCEPTION_NAME} Doesn't Throw {_ARGUMENT_NULL_EXCEPTION_NAME} For Non-Null Argument.")]
  public void NotNullOrThrowArgumentNullExceptionDoesNotThrowArgumentNullExceptionForNonNullArgument()
  {
    var expected = new object();

    var actual = expected.NotNullOrThrowArgumentNullException(nameof(expected));

    Assert.NotNull(actual);
    Assert.Equal(expected, actual);
  }

  /// <summary>
  /// </summary>
  [Fact(DisplayName =
      $@"{_CLASS_NAME} - Extension Method: {_METHOD_NOT_NULL_OR_THROW_ARGUMENT_NULL_EXCEPTION_NAME} Does Throw {_ARGUMENT_NULL_EXCEPTION_NAME} For Null Argument.")]
  public void NotNullOrThrowArgumentNullExceptionDoesThrowArgumentNullExceptionForNullArgument()
  {
    // ReSharper disable once RedundantAssignment
    // The unit test reads better by creating and initializing variable "expected".
    var expected = new object();
    const string _EXPECTED_ARGUMENT_NAME = nameof(expected);
    expected = null;
    var actual = Assert.Throws<ArgumentNullException>(() => expected.NotNullOrThrowArgumentNullException(_EXPECTED_ARGUMENT_NAME));

    Assert.NotNull(actual);
    Assert.NotEqual(expected, actual);
    Assert.IsType<ArgumentNullException>(actual);
    Assert.Equal(_EXPECTED_ARGUMENT_NAME, actual.ParamName);
    Assert.Contains(Expected._ARGUMENT_NULL_EXCEPTION_MESSAGE, actual.Message);
    Assert.Contains(_EXPECTED_ARGUMENT_NAME, actual.Message);

    actual = Assert.Throws<ArgumentNullException>(() => expected.NotNullOrThrowArgumentNullException());
    Assert.NotNull(actual);
    Assert.NotEqual(expected, actual);
    Assert.IsType<ArgumentNullException>(actual);
    Assert.Equal(Expected._DEFAULT_ARGUMENT_NAME, actual.ParamName);
    Assert.Contains(Expected._ARGUMENT_NULL_EXCEPTION_MESSAGE, actual.Message);
    Assert.Contains(Expected._DEFAULT_ARGUMENT_NAME, actual.Message);
  }

  /// <summary>
  /// </summary>
  [Fact(DisplayName =
      $@"{_CLASS_NAME} - Extension Method: {_METHOD_VALID_OR_THROW_ARGUMENT_EXCEPTION_NAME} Doesn't Throw {_ARGUMENT_EXCEPTION_NAME} For Delegate That Evaluates To True.")]
  public void ValidOrThrowArgumentExceptionDoesNotThrowArgumentExceptionForInValidArgument()
  {
    var expected = new object();
    var fakeDelegate = A.Fake<Func<bool?>>();

    A.CallTo(() => fakeDelegate()).Returns(true);

    var actual = expected.ValidOrThrowArgumentException(fakeDelegate, nameof(expected));

    Assert.NotNull(actual);
    Assert.Equal(expected, actual);
    A.CallTo(() => fakeDelegate()).MustHaveHappenedOnceExactly();
  }

  /// <summary>
  /// </summary>
  /// <param name="argument"></param>
  [InlineData(false)]
  [InlineData(null)]
  [Theory(DisplayName =
      $@"{_CLASS_NAME} - Extension Method: {_METHOD_VALID_OR_THROW_ARGUMENT_EXCEPTION_NAME} Throws {_ARGUMENT_EXCEPTION_NAME} For Delegate That Doesn't Evaluate To True.")]
  public void ValidOrThrowArgumentExceptionDoesThrowArgumentExceptionForInvalidArgument(bool? argument)
  {
    var expected = new object();
    var fakeDelegate = A.Fake<Func<bool?>>();
    const string _EXPECTED_ARGUMENT_NAME = nameof(expected);

    A.CallTo(() => fakeDelegate()).Returns(argument);

    var actual = Assert.Throws<ArgumentException>(() => expected.ValidOrThrowArgumentException(fakeDelegate, _EXPECTED_ARGUMENT_NAME));

    Assert.NotNull(actual);
    Assert.NotEqual(expected, actual);
    Assert.IsType<ArgumentException>(actual);
    Assert.Equal(_EXPECTED_ARGUMENT_NAME, actual.ParamName);
    Assert.Contains(Expected._ARGUMENT_INVALID_EXCEPTION_MESSAGE, actual.Message);
    Assert.Contains(_EXPECTED_ARGUMENT_NAME, actual.Message);
    A.CallTo(() => fakeDelegate()).MustHaveHappenedOnceExactly();

    Fake.ClearRecordedCalls(fakeDelegate);
    actual = Assert.Throws<ArgumentException>(() => expected.ValidOrThrowArgumentException(fakeDelegate));

    Assert.NotNull(actual);
    Assert.NotEqual(expected, actual);
    Assert.IsType<ArgumentException>(actual);
    Assert.Equal(Expected._DEFAULT_ARGUMENT_NAME, actual.ParamName);
    Assert.Contains(Expected._ARGUMENT_INVALID_EXCEPTION_MESSAGE, actual.Message);
    Assert.Contains(Expected._DEFAULT_ARGUMENT_NAME, actual.Message);
    A.CallTo(() => fakeDelegate()).MustHaveHappenedOnceExactly();
  }

  /// <summary>
  /// </summary>
  [Fact(DisplayName =
      $@"{_CLASS_NAME} - Extension Method: {_METHOD_VALID_OR_THROW_ARGUMENT_EXCEPTION_NAME} Throws {_ARGUMENT_NULL_EXCEPTION_NAME} For Null Delegate Test.")]
  public void ValidOrThrowArgumentExceptionThrowsArgumentNullExceptionForNullDelegateTest()
  {
    const string _EXPECTED_ARGUMENT_NAME = "argumentValidation";
    var expected = new object();

    var actual = Assert.Throws<ArgumentNullException>(() => expected.ValidOrThrowArgumentException(null, nameof(expected)));

    Assert.NotNull(actual);
    Assert.NotEqual(expected, actual);
    Assert.IsType<ArgumentNullException>(actual);
    Assert.Equal(_EXPECTED_ARGUMENT_NAME, actual.ParamName);
    Assert.Contains(Expected._ARGUMENT_NULL_EXCEPTION_MESSAGE, actual.Message);
    Assert.Contains(_EXPECTED_ARGUMENT_NAME, actual.Message);
  }
}