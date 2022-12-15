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

namespace Navient.Presentation.Service.Rest.Services;

/// <summary>
/// </summary>
public interface IClientInformationService
{
  /// <summary>
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  Client Create(CreateClientsInfoRequest model);

  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  ClientsInfo GetById(int id);

  /// <summary>
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  ClientsInfo GetByName(string name);

  /// <summary>
  /// </summary>
  /// <returns></returns>
  IEnumerable<Client> GetAll();

  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  string Delete(int id);

  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <param name="model"></param>
  void Update(int id, UpdateClientsInfoRequest model);
}

/// <summary>
/// </summary>
public class ClientInformationService : IClientInformationService
{
  private readonly IAuthorizationService _authService;
  private readonly DataContext _context;
  private readonly ILogger _logger;
  private readonly IChatTranscriptService _logService;
  private const string _LOG_CLIENT_LIST = "UserID {0} has access to {1} clients";

  /// <summary>
  /// </summary>
  /// <param name="logService"></param>
  /// <param name="context"></param>
  /// <param name="authService"></param>
  /// <param name="logger"></param>
  public ClientInformationService(IChatTranscriptService logService, DataContext context, IAuthorizationService authService, ILogger<ClientInformationService> logger)
  {
    _logService = logService;
    _context = context;
    _authService = authService;
    _logger = logger;
  }

  private ClientsInfo GetClientsInfo(int id)
  {
    var clientsInfo = _context?.ClientsInfo?.Find(id);
    if (clientsInfo != null) return clientsInfo;
    var error = $"A ClientsInfo with the id '{id}' doesn't exist!";
    _logService.LogError(Guid.Empty, ComponentTypes.InstantAssistant, error);
    throw new KeyNotFoundException(error);
  }

  private ClientsInfo GetClientsInfo(string name)
  {
    var clientsInfo = _context?.ClientsInfo?.Single(c => c.ChatBotName.Contains(name));
    if (clientsInfo != null) return clientsInfo;
    var error = $"A ClientsInfo with the name '{name}' doesn't exist!";
    _logService.LogError(Guid.Empty, ComponentTypes.InstantAssistant, error);
    throw new KeyNotFoundException(error);
  }

  /// <summary>
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  /// <exception cref="AppException"></exception>
  public Client Create(CreateClientsInfoRequest model)
  {
    if (_context?.ClientsInfo?.Any(x => x.ChatBotName == model.ChatBotName) is true)
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

    _context?.ClientsInfo?.Add(clientsInfo);
    _context?.SaveChanges();

    return new Client
    {
      Id = clientsInfo.ChatBotID,
      Name = clientsInfo.ChatBotName
    };
  }

  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public string Delete(int id)
  {
    var clientsInfo = GetClientsInfo(id);
    _context?.ClientsInfo?.Remove(clientsInfo);
    _context?.SaveChanges();
    return clientsInfo != null ? clientsInfo.ChatBotName : "";
  }

  /// <summary>
  /// </summary>
  /// <returns></returns>
  public IEnumerable<Client> GetAll()
  {
    var clientList = _context?.ClientsInfo?.Where(c => _authService.IsAdminAuthorized(c.ChatBotID)).Select(c => new Client
    {
      Id = c.ChatBotID,
      Name = c.ChatBotName,
      Logo = c.IAConfigData.displayConfig.logo
    })
        .ToList();

    if (clientList is null) return null;
    var clientsToLog = new ArrayList();
    foreach (var client in clientList) clientsToLog.Add(client.Name);

    _logger.LogInformation(string.Format(_LOG_CLIENT_LIST, _authService.GetUserId(), string.Join(", ", clientsToLog.Cast<string>().ToArray())));
    return clientList;
  }

  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public ClientsInfo GetById(int id)
  {
    return GetClientsInfo(id);
  }

  /// <summary>
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public ClientsInfo GetByName(string name)
  {
    return GetClientsInfo(name);
  }

  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <param name="model"></param>
  /// <exception cref="AppException"></exception>
  public void Update(int id, UpdateClientsInfoRequest model)
  {
    var clientsInfo = GetClientsInfo(id);

    if (clientsInfo is null) return;

    if (model.ChatBotName != clientsInfo.ChatBotName && _context?.ClientsInfo?.Any(x => x.ChatBotName == model.ChatBotName) is true)
    {
      var error = $"A ClientsInfo with the name '{model.ChatBotName}' already exists!";
      _logService.LogError(Guid.Empty, ComponentTypes.InstantAssistant, error);
      throw new AppException(error);
    }

    clientsInfo.ChatBotName = model.ChatBotName;
    clientsInfo.IAConfigData = model.ArtificialIntelligenceConfigurationData;
    if (clientsInfo.IAConfigData is not null)
      clientsInfo.IAConfigData.updatedBy = _authService.GetUserId();

    _context?.ClientsInfo?.Update(clientsInfo);
    _context?.SaveChanges();
  }
}