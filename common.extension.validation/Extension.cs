// 
//    Project:  common.extension.validation
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
public static class Extension
{
  /// <summary>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="parameter"></param>
  /// <param name="argumentName"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T NotNullOrThrowArgumentNullException<T>(this T parameter, string argumentName = DEFAULT_ARGUMENT_NAME)
  {
    if (parameter is not null)
      return parameter;


    throw new ArgumentNullException(argumentName, $@"{argumentName} {ARGUMENT_NULL_EXCEPTION_MESSAGE}");
  }

  /// <summary>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="parameter"></param>
  /// <param name="argumentValidation"></param>
  /// <param name="argumentName"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  public static T ValidOrThrowArgumentException<T>(this T parameter, Func<bool?> argumentValidation, string argumentName = DEFAULT_ARGUMENT_NAME)
  {
    argumentValidation.NotNullOrThrowArgumentNullException(nameof(argumentValidation));

    if (argumentValidation() is true)
      return parameter;

    throw new ArgumentException($@"{argumentName} {ARGUMENT_INVALID_EXCEPTION_MESSAGE}", argumentName);
  }
}