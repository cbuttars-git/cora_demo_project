namespace InstantAssistant.Api.Services;

using InstantAssistant.Api.Entities;
using InstantAssistant.Api.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Cryptography;

public interface IAuthService
{
  string GetUserId();
  bool IsAuthorized(string groupName);
  bool IsUserAuthorized(string clientName);
  bool IsUserAuthorized(int clientId);
  bool IsAdminAuthorized(string clientName);
  bool IsAdminAuthorized(int clientId);
}

public class AuthService : IAuthService
{
  protected readonly IConfiguration Configuration;
  protected readonly String AdminGroup;
  private DataContext _context;
  
  private IHttpContextAccessor _httpContextAccessor;

  private readonly ILogger _logger;

  private static readonly string _LogUserId = "UserID: {0}";
  private static readonly string _LogIsAuthorized = "UserID {0}, GroupName {1} - IsAuthorized: {2}";

  public AuthService(IConfiguration configuration, DataContext context, IHttpContextAccessor httpContextAccessor, ILogger<AuthService> logger)
  {
    Configuration = configuration;
    AdminGroup = Configuration.GetValue<String>("InstantAssistant:AdminGroup");

    _context = context;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
  }

  public string GetUserId()
  {
    // Add error logging when this gets implemented again
    var userId = _httpContextAccessor.HttpContext.User.Identity.Name.Split("\\")[1];
    // _logger.LogInformation(string.Format(_LogUserId, userId));
    return userId;
  }

  public bool IsAuthorized(string groupName)
  {
    //return true;

    // Add error logging when this gets implemented again
    if (String.IsNullOrEmpty(groupName) || groupName.ToLower() != "none")
    {
      var isAuthorized = _httpContextAccessor.HttpContext.User.IsInRole(AdminGroup) || _httpContextAccessor.HttpContext.User.IsInRole(groupName);
      _logger.LogInformation(String.Format(_LogIsAuthorized, GetUserId(), groupName, isAuthorized));
      return isAuthorized;
    }
    else
    {
      _logger.LogInformation(String.Format(_LogIsAuthorized, GetUserId(), groupName, true));
      return true;
    }
  }

  public bool IsUserAuthorized(string clientName)
  {
    try
    {
      var client = getClientsInfo(clientName);
      return IsAuthorized(client.IAConfigData.userAdGroup);
    }
    catch (KeyNotFoundException ex)
    {
      return false;
    }
  }

  public bool IsUserAuthorized(int clientId)
  {
    try
    {
      var client = getClientsInfo(clientId);
      return IsAuthorized(client.IAConfigData.userAdGroup);
    }
    catch (KeyNotFoundException ex)
    {
      return false;
    }
  }

  public bool IsAdminAuthorized(string clientName)
  {
    try
    {
      var client = getClientsInfo(clientName);
      return IsAuthorized(client.IAConfigData.adminAdGroup);
    }
    catch (KeyNotFoundException ex)
    {
      return false;
    }
  }

  public bool IsAdminAuthorized(int clientId)
  {
    try
    {
      var client = getClientsInfo(clientId);
      return IsAuthorized(client.IAConfigData.adminAdGroup);
    }
    catch (KeyNotFoundException ex)
    {
      return false;
    }
  }

  private ClientsInfo getClientsInfo(string name)
  {
    var clientsInfo = _context.ClientsInfo.SingleOrDefault(c => c.ChatBotName.Equals(name));
    if (clientsInfo == null) throw new KeyNotFoundException();
    return clientsInfo;
  }

  private ClientsInfo getClientsInfo(int cid)
  {
    var clientsInfo = _context.ClientsInfo.SingleOrDefault(c => c.ChatBotID.Equals(cid));
    if (clientsInfo == null) throw new KeyNotFoundException();
    return clientsInfo;
  }
}
