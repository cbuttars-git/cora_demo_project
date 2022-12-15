// 
//    Project:  Service.Handler.ParseAndReplaceMessage.Test
//    Created:  01-2022-11
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

using System.Collections.Generic;
using System.Linq;
using Navient.Presentation.Service.Handler;

namespace Navient.Test.Presentation.Service.Handler;

/// <summary>
/// </summary>
public class ExtensionTest
{
  private const string _CLASS_NAME = nameof(ExtensionTest);

  /// <summary>
  /// </summary>
  [Fact(DisplayName = $@"{_CLASS_NAME} Class: {nameof(GetBracketPositions)} Returns Expected String List")]
  public void GetBracketPositions()
  {
    const string _EXPECTED_TEST_MESSAGE = @"Another Slightly ~larger.message.here~. This is my ~test.message~ one";

    var expectedCharacterArray = _EXPECTED_TEST_MESSAGE.GetMessageAsArrayOfCharacters();
    var actualTokenPositions = expectedCharacterArray.GetBracketPositions();

    Assert.NotNull(actualTokenPositions);
    Assert.Equal(4, actualTokenPositions.Count);
    Assert.True(actualTokenPositions.IsValidTokenPairs());
    Assert.Equal(18, actualTokenPositions[0]);
    Assert.Equal(37, actualTokenPositions[1]);
    Assert.Equal(52, actualTokenPositions[2]);
    Assert.Equal(64, actualTokenPositions[3]);

    const string _BROKEN_EXPECTED_TEST_MESSAGE = @"Another Slightly ~larger.message.here~. This is my test.message~ one";

    expectedCharacterArray = _BROKEN_EXPECTED_TEST_MESSAGE.GetMessageAsArrayOfCharacters();
    actualTokenPositions = expectedCharacterArray.GetBracketPositions();

    Assert.NotNull(actualTokenPositions);
    Assert.Equal(3, actualTokenPositions.Count);
    Assert.False(actualTokenPositions.IsValidTokenPairs());
    Assert.Equal(18, actualTokenPositions[0]);
    Assert.Equal(37, actualTokenPositions[1]);
    Assert.Equal(64, actualTokenPositions[2]);
  }

  /// <summary>
  /// </summary>
  [Fact(DisplayName = $@"{_CLASS_NAME} Class: {nameof(GetTokenPartitionsTest)} Returns Expected Token Set")]
  public void GetTokenPartitionsTest()
  {
    var expectedTokenIndexes = new List<int> { 1, 10, 2, 20, 3, 30 };

    var actual = expectedTokenIndexes.GetTokenPartitions();

    Assert.NotNull(actual);
    Assert.True(actual.Any());
    Assert.Equal(expectedTokenIndexes.Count / 2, actual.Count);

    Assert.Equal(expectedTokenIndexes[0], actual[0].StartIndex);
    Assert.Equal(expectedTokenIndexes[1], actual[0].EndIndex);

    Assert.Equal(expectedTokenIndexes[2], actual[1].StartIndex);
    Assert.Equal(expectedTokenIndexes[3], actual[1].EndIndex);

    Assert.Equal(expectedTokenIndexes[4], actual[2].StartIndex);
    Assert.Equal(expectedTokenIndexes[5], actual[2].EndIndex);
  }

  /// <summary>
  /// </summary>
  [Fact(DisplayName = $@"{_CLASS_NAME} Class: {nameof(GetTokensFromMessage)} Returns Expected String List")]
  public void GetTokensFromMessage()
  {
    const string _EXPECTED_TOKEN = @"test.message";
    const string _EXPECTED_TEST_MESSAGE = $@"This is my ~{_EXPECTED_TOKEN}~ one";
    var expectedTokenPartitionSet = new List<TokenPartition> { new() { EndIndex = 24, StartIndex = 12 } };

    var actual = _EXPECTED_TEST_MESSAGE.GetTokensFromMessage(expectedTokenPartitionSet);

    Assert.NotNull(actual);
    Assert.Single(actual);
    Assert.Equal(_EXPECTED_TOKEN, actual[0]);

    const string _BIGGER_EXPECTED_TOKEN = @"larger.message.here";
    const string _BIGGER_EXPECTED_TEST_MESSAGE = $@"Another Slightly ~{_BIGGER_EXPECTED_TOKEN}~. {_EXPECTED_TEST_MESSAGE}";

    expectedTokenPartitionSet.Clear();
    expectedTokenPartitionSet.Add(new TokenPartition { EndIndex = 37, StartIndex = 18 });
    expectedTokenPartitionSet.Add(new TokenPartition { EndIndex = 64, StartIndex = 52 });

    actual = _BIGGER_EXPECTED_TEST_MESSAGE.GetTokensFromMessage(expectedTokenPartitionSet);

    Assert.NotNull(actual);
    Assert.Equal(2, actual.Count);
    Assert.Equal(_BIGGER_EXPECTED_TOKEN, actual[0]);
    Assert.Equal(_EXPECTED_TOKEN, actual[1]);
  }
}