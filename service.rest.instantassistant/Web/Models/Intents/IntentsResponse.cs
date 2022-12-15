// 
//    Project:  rest.instantassistant
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

namespace Navient.Presentation.Service.Rest.Web.Models.Intents;

/// <summary>
/// </summary>
public class Results
{
  /// <summary>
  /// </summary>
  public decimal[] Similarity_Score { get; set; }

  /// <summary>
  /// </summary>
  public int[] New_Intents_ID { get; set; }

  /// <summary>
  /// </summary>
  public int[] New_Userexample_ID { get; set; }

  /// <summary>
  /// </summary>
  public int[] Old_Intent_ID { get; set; }

  /// <summary>
  /// </summary>
  public int[] Old_Userexample_ID { get; set; }

  /// <summary>
  /// </summary>
  public string[] New_Intent { get; set; }

  /// <summary>
  /// </summary>
  public string[] New_Userexample { get; set; }

  /// <summary>
  /// </summary>
  public string[] Old_Intent { get; set; }

  /// <summary>
  /// </summary>
  public string[] Old_Userexample { get; set; }
}

/// <summary>
/// </summary>
public class IntentsResponse
{
  /// <summary>
  /// </summary>
  public Results results { get; set; }

  /// <summary>
  /// </summary>
  [Required]
  public string message { get; set; }
}