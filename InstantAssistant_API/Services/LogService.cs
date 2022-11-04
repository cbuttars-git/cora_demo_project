namespace InstantAssistant.Api.Services;

using System.Text.Json;
using System.Text.RegularExpressions;
using InstantAssistant.Api.Entities;
using InstantAssistant.Api.Helpers;
using InstantAssistant.Api.Web.Models.Query;

public interface ILogService
{
    void LogChat(Guid sessionId, ComponentTypes componentId, QueryResponse log);
    void LogError(Guid sessionId, ComponentTypes componentId, Object log);
}

public class LogService : ILogService
{
  private DataContext _context;
  private IAuthService _authService;

  private readonly ILogger _logger;
  private static readonly string _LogError = "An error occured:";

  private readonly Regex htmlTags = new Regex(@"<[^>]*(>|$)");
  private readonly Regex unicode = new Regex(@"[^\u0000-\u007F]");

  public LogService(DataContext context, IAuthService authService, ILogger<LogService> logger)
  {
    _context = context;
    _authService = authService;
    _logger = logger;
  }

  public void LogChat(Guid sessionId, ComponentTypes componentId, QueryResponse log)
  {
    log.userId = _authService.GetUserId();

    var logJSON = JsonSerializer.Serialize(log);

    var rptLog = new QueryResponse
    {
      answer = htmlTags.Replace(unicode.Replace(log.answer, String.Empty), String.Empty),
      chatbot = log.chatbot,
      image_url = log.image_url,
      options = log.options,
      query = log.query,
      similar = log.similar,
      similar_q = log.similar_q,
      url = log.url,
      userId = log.userId,
    };

    var logRpt = JsonSerializer.Serialize(rptLog);

    var chatLog = new ChatBotLogs
    {
      SessionID = sessionId.ToString(),
      ComponentID = ((int)componentId),
      ChatLog = logJSON,
      ChatLogRpt = logRpt
    };

    _context.ChatBotLog.Add(chatLog);
    _context.SaveChanges();
  }

  public void LogError(Guid sessionId, ComponentTypes componentId, Object log)
  {
    _logger.LogError(_LogError, log);

    var errorLog = new ErrorLogs
    {
      SessionID = sessionId.ToString(),
      ComponentID = ((int)componentId),
      ErrorLog = JsonSerializer.Serialize(log)
    };

    _context.ErrorLog.Add(errorLog);
    _context.SaveChanges();
  }
}
