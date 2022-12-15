// 
//    Project:  Common.Model.Test
//    Created:  26-2022-10
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

namespace Navient.Test.Common.Model;

internal static class Constant
{
  internal const double EXPECTED_DOUBLE_PROPERTY_TYPE_VALUE = 142.5685d;
  internal const float EXPECTED_FLOAT_ARRAY_PROPERTY_VALUE_0 = 12456.9897465f;
  internal const float EXPECTED_FLOAT_ARRAY_PROPERTY_VALUE_1 = float.MaxValue;
  internal const float EXPECTED_FLOAT_ARRAY_PROPERTY_VALUE_2 = 555.555555f;
  internal const int EXPECTED_INTEGER_PROPERTY_VALUE = 99999;
  internal const string EXPECTED_JSON_PROPERTY_TEST_NAME = @"ThisIsATest";
  internal const string EXPECTED_STRING_LIST_PROPERTY_VALUE_0 = @"This";
  internal const string EXPECTED_STRING_LIST_PROPERTY_VALUE_1 = @"is";
  internal const string EXPECTED_STRING_LIST_PROPERTY_VALUE_2 = @"a";
  internal const string EXPECTED_STRING_LIST_PROPERTY_VALUE_3 = @"list";
  internal const string EXPECTED_STRING_PROPERTY_VALUE = @"Oh What a Test";

  internal static DateTime ExpectedDateTimeProperty = new(2020, 05, 25, 22, 43, 39);
}