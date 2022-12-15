﻿// 
//    Project:  model.applicationaettings.abstraction
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

namespace Navient.Presentation.Configuration.Model.Serilog;

/// <summary>
/// </summary>
public interface IArgumentsModel : IJsonBase
{
  /// <summary>
  /// </summary>
  bool? Buffered { get; set; }

  /// <summary>
  /// </summary>
  bool? RollOnFileSizeLimit { get; set; }

  /// <summary>
  /// </summary>
  int? RetainedFileCountLimit { get; set; }

  /// <summary>
  /// </summary>
  long? FileSizeLimitBytes { get; set; }

  /// <summary>
  /// </summary>
  RollingInterval? RollingInterval { get; set; }

  /// <summary>
  /// </summary>
  string OutputTemplate { get; set; }

  /// <summary>
  /// </summary>
  string Path { get; set; }
}