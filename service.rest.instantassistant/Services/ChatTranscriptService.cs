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
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Navient.Presentation.Service.Rest.Services;

/// <summary>
/// </summary>
public interface IChatTranscriptService
{
  /// <summary>
  /// </summary>
  /// <param name="sessionId"></param>
  /// <param name="componentId"></param>
  /// <param name="log"></param>
  void LogChat(Guid sessionId, ComponentTypes componentId, QueryResponse log);

  /// <summary>
  /// </summary>
  /// <param name="sessionId"></param>
  /// <param name="componentId"></param>
  /// <param name="log"></param>
  void LogError(Guid sessionId, ComponentTypes componentId, object log);
}

/// <summary>
/// </summary>
public class ChatTranscriptService : IChatTranscriptService
{
  private readonly IAuthorizationService _authService;
  private readonly DataContext _context;

  private readonly Regex _htmlTags = new(@"<[^>]*(>|$)");
  private readonly ILogger _logger;
  private readonly Regex _unicode = new(@"[^\u0000-\u007F]");
  private const string _LOG_ERROR = "An error occured:";

  /// <summary>
  /// </summary>
  /// <param name="context"></param>
  /// <param name="authService"></param>
  /// <param name="logger"></param>
  public ChatTranscriptService(DataContext context, IAuthorizationService authService, ILogger<ChatTranscriptService> logger)
  {
    _context = context;
    _authService = authService;
    _logger = logger;
  }

  /// <summary>
  /// </summary>
  /// <param name="sessionId"></param>
  /// <param name="componentId"></param>
  /// <param name="log"></param>
  public void LogChat(Guid sessionId, ComponentTypes componentId, QueryResponse log)
  {
    log.userId = _authService.GetUserId();

    var rptLog = new QueryResponse
    {
      answer = _htmlTags.Replace(_unicode.Replace(log.answer, string.Empty), string.Empty),
      chatbot = log.chatbot,
      image_url = log.image_url,
      options = log.options,
      query = log.query,
      similar = log.similar,
      similar_q = log.similar_q,
      url = log.url,
      userId = log.userId
    };

    var chatLog = new ChatBotLog
    {
      SessionId = sessionId.ToString(),
      ComponentId = (int)componentId,
      ChatTranscript = JsonSerializer.Serialize(log),
      ChatTranscriptReport = JsonSerializer.Serialize(rptLog)
    };

    _context.ChatBotLog?.Add(chatLog);
    _context.SaveChanges();
  }

  /// <summary>
  /// </summary>
  /// <param name="sessionId"></param>
  /// <param name="componentId"></param>
  /// <param name="log"></param>
  public void LogError(Guid sessionId, ComponentTypes componentId, object log)
  {
    _logger.LogError(_LOG_ERROR, log);

    var errorLog = new ErrorLogs
    {
      SessionID = sessionId.ToString(),
      ComponentID = (int)componentId,
      ErrorLog = JsonSerializer.Serialize(log)
    };

    _context.ErrorLog?.Add(errorLog);
    _context.SaveChanges();
  }
}