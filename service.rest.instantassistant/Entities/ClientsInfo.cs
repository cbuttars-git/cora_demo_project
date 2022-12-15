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

namespace Navient.Presentation.Service.Rest.Entities;

/// <summary>
/// </summary>
[Table("ClientsInfo", Schema = "Config")]
public class ClientsInfo
{
  /// <summary>
  /// </summary>
  public IAConfigData IAConfigData { get; set; }

  /// <summary>
  /// </summary>
  [Key]
  public int ChatBotID { get; set; }

  /// <summary>
  /// </summary>
  public string ChatBotName { get; set; }
}

/// <summary>
/// </summary>
[NotMapped]
public class Client
{
  /// <summary>
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// </summary>
  public string Logo { get; set; }

  /// <summary>
  /// </summary>
  public string Name { get; set; }
}

/// <summary>
/// </summary>
[Keyless]
public class IAConfigData
{
  /// <summary>
  /// </summary>
  public bool active { get; set; }

  /// <summary>
  /// </summary>
  public DisplayConfig displayConfig { get; set; }

  /// <summary>
  /// </summary>
  public Intent errorIntent { get; set; }

  /// <summary>
  /// </summary>
  public Intent welcomeIntent { get; set; }

  /// <summary>
  /// </summary>
  public Intent[] intents { get; set; }

  /// <summary>
  /// </summary>
  public string adminAdGroup { get; set; }

  /// <summary>
  /// </summary>
  public string description { get; set; }

  /// <summary>
  /// </summary>
  public string placeholder { get; set; }

  /// <summary>
  /// </summary>
  public string updatedBy { get; set; }

  /// <summary>
  /// </summary>
  public string userAdGroup { get; set; }
}

/// <summary>
/// </summary>
[Keyless]
public class DisplayConfig
{
  /// <summary>
  /// </summary>
  public string dialogBgColor { get; set; }

  /// <summary>
  /// </summary>
  public string dialogFgColor { get; set; }

  /// <summary>
  /// </summary>
  public string footerBgColor { get; set; }

  /// <summary>
  /// </summary>
  public string footerFgColor { get; set; }

  /// <summary>
  /// </summary>
  public string headerBgColor { get; set; }

  /// <summary>
  /// </summary>
  public string headerFgColor { get; set; }

  /// <summary>
  /// </summary>
  public string linkColor { get; set; }

  /// <summary>
  /// </summary>
  public string logo { get; set; }
}

/// <summary>
/// </summary>
[Keyless]
public class Intent
{
  /// <summary>
  /// </summary>
  public bool active { get; set; }

  /// <summary>
  /// </summary>
  public bool aIModelDeploy { get; set; }

  /// <summary>
  /// </summary>
  public bool updated { get; set; }

  /// <summary>
  /// </summary>
  public int id { get; set; }

  /// <summary>
  /// </summary>
  public string aIModelDeployDate { get; set; }

  /// <summary>
  /// </summary>
  public string aIModelTestUpdateDate { get; set; }

  /// <summary>
  /// </summary>
  public string name { get; set; }

  /// <summary>
  /// </summary>
  public string response { get; set; }

  /// <summary>
  /// </summary>
  public string url { get; set; }

  /// <summary>
  /// </summary>
  public UserExample[] userExamples { get; set; }
}

/// <summary>
/// </summary>
[Keyless]
public class UserExample
{
  /// <summary>
  /// </summary>
  public bool active { get; set; }

  /// <summary>
  /// </summary>
  public bool updated { get; set; }

  /// <summary>
  /// </summary>
  public int id { get; set; }

  /// <summary>
  /// </summary>
  public string aIModelDeployDate { get; set; }

  /// <summary>
  /// </summary>
  public string aIModelTestDate { get; set; }

  /// <summary>
  /// </summary>
  public string text { get; set; }
}