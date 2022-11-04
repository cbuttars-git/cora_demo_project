namespace InstantAssistant.Api.Services;

using InstantAssistant.Api.Entities;
using InstantAssistant.Api.Helpers;
using InstantAssistant.Api.Web.Models.Query;

public interface ISessionService
{
    Session NewSession(string clientName);
}

public class Session
{
  public Guid ID { get; set; }
  public ClientsInfo Config { get; set; }
}

public class SessionService : ISessionService
{
  private ILogService _logService;
  private DataContext _context;

  public SessionService(ILogService logService, DataContext context)
  {
    _logService = logService;
    _context = context;
  }

  public Session NewSession(string clientName)
  {
    var config = getClientsInfo(clientName);

    var sessionId = Guid.NewGuid();

    _logService.LogChat(sessionId, ComponentTypes.AgentAssistChatBot, new QueryResponse
    {
      answer = "New Session",
      chatbot = config.ChatBotID.ToString()
    });

    return new Session
    {
      ID = sessionId,
      Config = config
    };
  }

  private ClientsInfo getClientsInfo(string name)
  {
    var clientsInfo = _context.ClientsInfo.SingleOrDefault(c => c.ChatBotName.Equals(name));
    if (clientsInfo == null) throw new KeyNotFoundException("ClientsInfo not found");
    return clientsInfo;
  }
}
