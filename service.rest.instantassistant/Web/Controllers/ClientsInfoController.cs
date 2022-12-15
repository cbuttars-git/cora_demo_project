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

using Navient.Presentation.Service.Rest.Web.Models.ClientsInfo;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Navient.Presentation.Service.Rest.Web.Controllers;

/// <summary>
/// </summary>
[ApiController]
[Route("InstantAssistant/[controller]")]
public class ClientsInfoController : ControllerBase
{
  private readonly string _adminGroup;
  private readonly IAuthorizationService _authService;
  private readonly IClientInformationService _clientsInfoService;
  private readonly ILogger _logger;

  private const string _LOG_CANNOT_CREATE = "User is not authorized to create new clients";
  private const string _LOG_CANNOT_DELETE = "User is not authorized to delete client {0}";
  private const string _LOG_CANNOT_UPDATE = "User is not authorized to update client {0}";
  private const string _LOG_CLIENT_FOUND = "Found and returning data for client {0}";
  private const string _LOG_CREATED_CLIENT = "Successfully created {0}";
  private const string _LOG_DELETED_CLIENT = "Successfully deleted {0}";
  private const string _LOG_UNAUTHORIZED = "User is not authorized to administer client {0}";
  private const string _LOG_UPDATED_CLIENT = "Successfully updated {0}";

  /// <summary>
  /// </summary>
  /// <param name="configuration"></param>
  /// <param name="service"></param>
  /// <param name="authService"></param>
  /// <param name="logger"></param>
  public ClientsInfoController(IConfiguration configuration, IClientInformationService service, IAuthorizationService authService, ILogger<ClientsInfoController> logger)
  {
    // It is bad mojo to pass around IConfiguration
    _adminGroup = configuration.GetValue<string>("InstantAssistant:AdminGroup");
    _clientsInfoService = service;
    _authService = authService;
    _logger = logger;
  }

  /// <summary>
  /// </summary>
  /// <param name="clientsInfo"></param>
  /// <returns></returns>
  [HttpPost]
  public IActionResult Create(CreateClientsInfoRequest clientsInfo)
  {
    var authorized = _authService.IsAuthorized(_adminGroup);
    if (!authorized)
    {
      _logger.LogError(_LOG_CANNOT_CREATE);
      return Unauthorized(_LOG_CANNOT_CREATE);
    }

    try
    {
      var client = _clientsInfoService.Create(clientsInfo);
      _logger.LogInformation(string.Format(_LOG_CREATED_CLIENT, client.Name));
      return Ok(client);
    }
    catch (AppException ex)
    {
      _logger.LogError(ex.Message);
      return Conflict(ex.Message);
    }
  }

  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpDelete("{id}")]
  public IActionResult Delete(int id)
  {
    var authorized = _authService.IsAdminAuthorized(id);
    if (!authorized)
    {
      var log = string.Format(_LOG_CANNOT_DELETE, id);
      _logger.LogError(log);
      return Unauthorized(log);
    }
    else
    {
      var chatBotName = _clientsInfoService.Delete(id);
      var log = string.Format(_LOG_DELETED_CLIENT, chatBotName);
      _logger.LogInformation(log);
      return Ok(log);
    }
  }

  /// <summary>
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  public IActionResult GetAll()
  {
    var clientsInfo = _clientsInfoService.GetAll();
    return Ok(clientsInfo);
  }

  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet(@"{id}")]
  public IActionResult GetById(int id)
  {
    var authorized = _authService.IsAdminAuthorized(id);
    if (!authorized)
    {
      var log = string.Format(_LOG_UNAUTHORIZED, id);
      _logger.LogError(log);
      return Unauthorized(log);
    }

    try
    {
      var clientsInfo = _clientsInfoService.GetById(id);
      _logger.LogInformation(string.Format(_LOG_CLIENT_FOUND, clientsInfo.ChatBotName));
      return Ok(clientsInfo);
    }
    catch (KeyNotFoundException ex)
    {
      _logger.LogError(ex.Message);
      _logger.LogError(ex.Message);
      return NotFound(ex.Message);
    }
  }

  /// <summary>
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  [HttpGet("ByName/{name}")]
  public IActionResult GetByName(string name)
  {
    var authorized = _authService.IsAdminAuthorized(name);
    if (!authorized)
    {
      var log = string.Format(_LOG_UNAUTHORIZED, name);
      _logger.LogError(log);
      return Unauthorized(log);
    }

    try
    {
      var clientsInfo = _clientsInfoService.GetByName(name);
      _logger.LogInformation(string.Format(_LOG_CLIENT_FOUND, clientsInfo.ChatBotName));
      return Ok(clientsInfo);
    }
    catch (KeyNotFoundException ex)
    {
      _logger.LogError(ex.Message);
      _logger.LogError(ex.Message);
      return NotFound(ex.Message);
    }
  }

  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <param name="clientsInfo"></param>
  /// <returns></returns>
  [HttpPut("{id}")]
  public IActionResult Update(int id, UpdateClientsInfoRequest clientsInfo)
  {
    var authorized = _authService.IsAdminAuthorized(id);
    if (!authorized)
    {
      var log = string.Format(_LOG_CANNOT_UPDATE, clientsInfo.ChatBotName);
      _logger.LogError(log);
      return Unauthorized(log);
    }

    try
    {
      _clientsInfoService.Update(id, clientsInfo);
      var log = string.Format(_LOG_UPDATED_CLIENT, clientsInfo.ChatBotName);
      _logger.LogInformation(log);
      return Ok(log);
    }
    catch (AppException ex)
    {
      _logger.LogError(ex.Message);
      return Conflict(ex.Message);
    }
  }
}