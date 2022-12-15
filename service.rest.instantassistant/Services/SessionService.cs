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

using Navient.Presentation.Service.Rest.Web.Models.Query;

namespace Navient.Presentation.Service.Rest.Services;

/// <summary>
/// </summary>
public interface ISessionService
{
  /// <summary>
  /// </summary>
  /// <param name="clientName"></param>
  /// <returns></returns>
  Session NewSession(string clientName);
}

/// <summary>
/// </summary>
public class Session
{
  /// <summary>
  /// </summary>
  public ClientsInfo Config { get; set; }

  /// <summary>
  /// </summary>
  public Guid Id { get; set; }
}

/// <summary>
/// </summary>
public class SessionService : ISessionService
{
  private readonly DataContext _context;
  private readonly IChatTranscriptService _logService;

  /// <summary>
  /// </summary>
  /// <param name="logService"></param>
  /// <param name="context"></param>
  public SessionService(IChatTranscriptService logService, DataContext context)
  {
    _logService = logService;
    _context = context;
  }

  private ClientsInfo GetClientsInfo(string name)
  {
    var clientsInfo = _context.ClientsInfo?.SingleOrDefault(c => c.ChatBotName.Equals(name));
    if (clientsInfo == null) throw new KeyNotFoundException("ClientsInfo not found");
    return clientsInfo;
  }

  /// <summary>
  /// </summary>
  /// <param name="clientName"></param>
  /// <returns></returns>
  public Session NewSession(string clientName)
  {
    var config = GetClientsInfo(clientName);

    var sessionId = Guid.NewGuid();

    _logService.LogChat(sessionId, ComponentTypes.AgentAssistChatBot, new QueryResponse
    {
      answer = "New Session",
      chatbot = config.ChatBotID.ToString()
    });

    return new Session
    {
      Id = sessionId,
      Config = config
    };
  }
}