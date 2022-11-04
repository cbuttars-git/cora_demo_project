namespace InstantAssistant.Api.Services;

using InstantAssistant.Api.Entities;
using InstantAssistant.Api.Helpers;
using InstantAssistant.Api.Web.Models.ClientsInfo;
using System.Collections;

public interface IClientsInfoService
{
    IEnumerable<Client> GetAll();
    ClientsInfo GetById(int id);
    ClientsInfo GetByName(string name);
    Client Create(CreateClientsInfoRequest model);
    void Update(int id, UpdateClientsInfoRequest model);
    string Delete(int id);
}

public class ClientsInfoService : IClientsInfoService
{
  private ILogService _logService;
  private DataContext _context;
  private IAuthService _authService;

  private readonly ILogger _logger;
  private static readonly string _LogClientList = "UserID {0} has access to {1} clients";

  public ClientsInfoService(ILogService logService, DataContext context, IAuthService authService, ILogger<ClientsInfoService> logger)
  {
    _logService = logService;
    _context = context;
    _authService = authService;
    _logger = logger;
  }

  public IEnumerable<Client> GetAll()
  {
    var clientList = _context.ClientsInfo.Select(c => new Client
      {
        Id = c.ChatBotID,
        Name = c.ChatBotName,
        Logo = c.IAConfigData.displayConfig.logo
      })
      .ToList<Client>()
      .Where(c => _authService.IsAdminAuthorized(c.Id));

    var clientsToLog = new ArrayList();
    foreach (var client in clientList)
    {
      clientsToLog.Add(client.Name);
    };

    _logger.LogInformation(String.Format(_LogClientList, _authService.GetUserId(), String.Join(", ", clientsToLog.Cast<string>().ToArray())));
    return clientList;
  }

  public ClientsInfo GetById(int id)
  {
    return getClientsInfo(id);
  }

  public ClientsInfo GetByName(string name)
  {
    return getClientsInfo(name);
  }

  public Client Create(CreateClientsInfoRequest model)
  {
    if (_context.ClientsInfo.Any(x => x.ChatBotName == model.ChatBotName))
    {
      var error = $"A ClientsInfo with the name '{model.ChatBotName}' already exists!";
      _logService.LogError(Guid.Empty, ComponentTypes.InstantAssistant, error);
      throw new AppException(error);
    }

    var clientsInfo = new ClientsInfo
    {
      ChatBotName = model.ChatBotName,
      IAConfigData = new IAConfigData
      {
        description = "",
        active = true,
        userAdGroup = "",
        adminAdGroup = "",
        placeholder = "",
        displayConfig = new DisplayConfig
        {
            headerBgColor = "",
            headerFgColor = "",
            footerBgColor = "",
            footerFgColor = "",
            dialogBgColor = "",
            dialogFgColor = "",
            linkColor = "",
            logo = ""
        },
        intents = new List<Intent>().ToArray(),
        updatedBy = _authService.GetUserId()
      }
    };

    _context.ClientsInfo.Add(clientsInfo);
    _context.SaveChanges();

    return new Client
    {
      Id = clientsInfo.ChatBotID,
      Name = clientsInfo.ChatBotName
    };
  }

  public void Update(int id, UpdateClientsInfoRequest model)
  {
    var clientsInfo = getClientsInfo(id);

    if (model.ChatBotName != clientsInfo.ChatBotName && _context.ClientsInfo.Any(x => x.ChatBotName == model.ChatBotName))
    {
      var error = $"A ClientsInfo with the name '{model.ChatBotName}' already exists!";
      _logService.LogError(Guid.Empty, ComponentTypes.InstantAssistant, error);
      throw new AppException(error);
    }

    clientsInfo.ChatBotName = model.ChatBotName;
    clientsInfo.IAConfigData = model.IAConfigData;

    clientsInfo.IAConfigData.updatedBy = _authService.GetUserId();

    _context.ClientsInfo.Update(clientsInfo);
    _context.SaveChanges();
  }

  public string Delete(int id)
  {
    var clientsInfo = getClientsInfo(id);
    _context.ClientsInfo.Remove(clientsInfo);
    _context.SaveChanges();
    return clientsInfo != null ? clientsInfo.ChatBotName : "";
  }

  private ClientsInfo getClientsInfo(int id)
  {
    var clientsInfo = _context.ClientsInfo.Find(id);
    if (clientsInfo == null)
    {
      var error = $"A ClientsInfo with the id '{id}' doesn't exist!";
      _logService.LogError(Guid.Empty, ComponentTypes.InstantAssistant, error);
      throw new KeyNotFoundException(error);
    }

    return clientsInfo;
  }

  private ClientsInfo getClientsInfo(string name)
  {
    var clientsInfo = _context.ClientsInfo.Single(c => c.ChatBotName.Contains(name));
    if (clientsInfo == null)
    {
      var error = $"A ClientsInfo with the name '{name}' doesn't exist!";
      _logService.LogError(Guid.Empty, ComponentTypes.InstantAssistant, error);
      throw new KeyNotFoundException(error);
    }
    return clientsInfo;
  }
}
