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

using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Navient.Presentation.Service.Rest.Services;

/// <summary>
/// </summary>
public interface IAuthorizationService
{
  /// <summary>
  /// </summary>
  /// <param name="clientName"></param>
  /// <returns></returns>
  bool IsAdminAuthorized(string clientName);

  /// <summary>
  /// </summary>
  /// <param name="clientId"></param>
  /// <returns></returns>
  bool IsAdminAuthorized(int clientId);

  /// <summary>
  /// </summary>
  /// <param name="groupName"></param>
  /// <returns></returns>
  bool IsAuthorized(string groupName);

  /// <summary>
  /// </summary>
  /// <param name="clientName"></param>
  /// <returns></returns>
  bool IsUserAuthorized(string clientName);

  /// <summary>
  /// </summary>
  /// <param name="clientId"></param>
  /// <returns></returns>
  bool IsUserAuthorized(int clientId);

  /// <summary>
  /// </summary>
  /// <returns></returns>
  string GetUserId();
}

/// <summary>
/// </summary>
public class AuthorizationService : IAuthorizationService
{
  private readonly string _adminGroup;
  private readonly DataContext _context;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly ILogger _logger;

  private const string _LOG_IS_AUTHORIZED = @"UserID {0}, GroupName {1} - IsAuthorized: {2}";
  //private static readonly string _LogUserId = "UserID: {0}";

  /// <summary>
  /// </summary>
  /// <param name="configuration"></param>
  /// <param name="context"></param>
  /// <param name="httpContextAccessor"></param>
  /// <param name="logger"></param>
  public AuthorizationService(IConfiguration configuration, DataContext context, IHttpContextAccessor httpContextAccessor, ILogger<AuthorizationService> logger)
  {
    //Configuration = configuration;
    _adminGroup = configuration.GetValue<string>("InstantAssistant:AdminGroup");

    _context = context;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
  }

  private ClientsInfo GetClientsInfo(string name)
  {
    var clientsInfo = _context?.ClientsInfo?.SingleOrDefault(c => c.ChatBotName != null && c.ChatBotName.Equals(name));
    if (clientsInfo == null) throw new KeyNotFoundException();
    return clientsInfo;
  }

  private ClientsInfo GetClientsInfo(int cid)
  {
    var clientsInfo = _context?.ClientsInfo?.SingleOrDefault(c => c.ChatBotID.Equals(cid));
    if (clientsInfo == null) throw new KeyNotFoundException();
    return clientsInfo;
  }

  /// <summary>
  /// </summary>
  /// <returns></returns>
  public string GetUserId()
  {
    // Add error logging when this gets implemented again
    var userId = _httpContextAccessor.HttpContext?.User.Identity?.Name?.Split("\\")[1];
    // _logger.LogInformation(string.Format(_LogUserId, userId));
    return userId;
  }

  /// <summary>
  /// </summary>
  /// <param name="clientName"></param>
  /// <returns></returns>
  public bool IsAdminAuthorized(string clientName)
  {
    try
    {
      var client = GetClientsInfo(clientName);
      return IsAuthorized(client?.IAConfigData?.adminAdGroup);
    }
    catch (KeyNotFoundException)
    {
      return false;
    }
  }

  /// <summary>
  /// </summary>
  /// <param name="clientId"></param>
  /// <returns></returns>
  public bool IsAdminAuthorized(int clientId)
  {
    try
    {
      var client = GetClientsInfo(clientId);
      return IsAuthorized(client?.IAConfigData?.adminAdGroup);
    }
    catch (KeyNotFoundException)
    {
      return false;
    }
  }

  /// <summary>
  /// </summary>
  /// <param name="groupName"></param>
  /// <returns></returns>
  public bool IsAuthorized(string groupName)
  {
    //return true;

    // Add error logging when this gets implemented again
    if (string.IsNullOrEmpty(groupName) || groupName.ToLower() != "none")
    {
      var isAuthorized = _httpContextAccessor.HttpContext?.User.IsInRole(_adminGroup) is true || _httpContextAccessor.HttpContext?.User.IsInRole(groupName ?? @"") is true;
      _logger.LogInformation(string.Format(_LOG_IS_AUTHORIZED, GetUserId(), groupName, isAuthorized));
      return isAuthorized;
    }

    _logger.LogInformation(string.Format(_LOG_IS_AUTHORIZED, GetUserId(), groupName, true));
    return true;
  }

  /// <summary>
  /// </summary>
  /// <param name="clientName"></param>
  /// <returns></returns>
  public bool IsUserAuthorized(string clientName)
  {
    try
    {
      var client = GetClientsInfo(clientName);
      return IsAuthorized(client?.IAConfigData?.userAdGroup);
    }
    catch (KeyNotFoundException)
    {
      return false;
    }
  }

  /// <summary>
  /// </summary>
  /// <param name="clientId"></param>
  /// <returns></returns>
  public bool IsUserAuthorized(int clientId)
  {
    try
    {
      var client = GetClientsInfo(clientId);
      return IsAuthorized(client?.IAConfigData?.userAdGroup);
    }
    catch (KeyNotFoundException)
    {
      return false;
    }
  }
}