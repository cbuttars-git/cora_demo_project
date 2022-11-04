namespace InstantAssistant.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using InstantAssistant.Api.Web.Models.ClientsInfo;
using InstantAssistant.Api.Services;
using InstantAssistant.Api.Helpers;

[ApiController]
[Route("InstantAssistant/[controller]")]
public class ClientsInfoController : ControllerBase
{
  protected readonly IConfiguration Configuration;
  protected readonly String AdminGroup;
  private IClientsInfoService _clientsInfoService;
  private IAuthService _authService;

  private readonly ILogger _logger;
  private static readonly string _LogUnauthorized = "User is not authorized to administer client {0}";
  private static readonly string _LogClientFound = "Found and returning data for client {0}";
  private static readonly string _LogCannotCreate = "User is not authorized to create new clients";
  private static readonly string _LogCreatedClient = "Successfully created {0}";
  private static readonly string _LogCannotUpdate = "User is not authorized to update client {0}";
  private static readonly string _LogUpdatedClient = "Successfully updated {0}";
  private static readonly string _LogCannotDelete = "User is not authorized to delete client {0}";
  private static readonly string _LogDeletedClient = "Successfully deleted {0}";

  public ClientsInfoController(IConfiguration configuration, IClientsInfoService service, IAuthService authService, ILogger<ClientsInfoController> logger)
  {
    Configuration = configuration;
    AdminGroup = Configuration.GetValue<String>("InstantAssistant:AdminGroup");

    _clientsInfoService = service;
    _authService = authService;
    _logger = logger;
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    var clientsInfo = _clientsInfoService.GetAll();
    return Ok(clientsInfo);
  }

  [HttpGet("{id}")]
  public IActionResult GetById(int id)
  {
    var authorized = _authService.IsAdminAuthorized(id);
    if(!authorized)
    {
      var log = String.Format(_LogUnauthorized, id);
      _logger.LogError(log);
      return Unauthorized(log);
    }

    try
    {
      var clientsInfo = _clientsInfoService.GetById(id);
      _logger.LogInformation(string.Format(_LogClientFound, clientsInfo.ChatBotName));
      return Ok(clientsInfo);
    }
    catch(KeyNotFoundException ex) {
      _logger.LogError(ex.Message);_logger.LogError(ex.Message);
      return NotFound(ex.Message);
    }
  }

  [HttpGet("ByName/{name}")]
  public IActionResult GetByName(string name)
  {
    var authorized = _authService.IsAdminAuthorized(name);
    if(!authorized)
    {
      var log = String.Format(_LogUnauthorized, name);
      _logger.LogError(log);
      return Unauthorized(log);
    }

    try
    {
      var clientsInfo = _clientsInfoService.GetByName(name);
      _logger.LogInformation(string.Format(_LogClientFound, clientsInfo.ChatBotName));
      return Ok(clientsInfo);
    }
    catch(KeyNotFoundException ex)
    {
      _logger.LogError(ex.Message); _logger.LogError(ex.Message);
      return NotFound(ex.Message);
    }
  }

  [HttpPost]
  public IActionResult Create(CreateClientsInfoRequest clientsInfo)
  {
    var authorized = _authService.IsAuthorized(AdminGroup);
    if(!authorized)
    {
      _logger.LogError(_LogCannotCreate);
      return Unauthorized(_LogCannotCreate);
    }

    try
    {
      var client = _clientsInfoService.Create(clientsInfo);
      _logger.LogInformation(string.Format(_LogCreatedClient, client.Name));
      return Ok(client);
    }
    catch (AppException ex)
    {
      _logger.LogError(ex.Message);
      return Conflict(ex.Message);
    }
  }

  [HttpPut("{id}")]
  public IActionResult Update(int id, UpdateClientsInfoRequest clientsInfo)
  {
    var authorized = _authService.IsAdminAuthorized(id);
    if(!authorized)
    {
      var log = string.Format(_LogCannotUpdate, clientsInfo.ChatBotName);
      _logger.LogError(log);
      return Unauthorized(log);
    }

    try
    {
      _clientsInfoService.Update(id, clientsInfo);
      var log = string.Format(_LogUpdatedClient, clientsInfo.ChatBotName);
      _logger.LogInformation(log);
      return Ok(log);
    }
    catch (AppException ex)
    {
      _logger.LogError(ex.Message);
      return Conflict(ex.Message);
    }
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(int id)
  {
    var authorized = _authService.IsAdminAuthorized(id);
    if (!authorized)
    {
      var log = string.Format(_LogCannotDelete, id);
      _logger.LogError(log);
      return Unauthorized(log);
    }
    else
    {
      var chatBotName = _clientsInfoService.Delete(id);
      var log = string.Format(_LogDeletedClient, chatBotName);
      _logger.LogInformation(log);
      return Ok(log);
    }
  }
}