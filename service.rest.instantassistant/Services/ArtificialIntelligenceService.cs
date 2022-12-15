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

using Navient.Presentation.Service.Rest.Web.Models.Intents;
using Navient.Presentation.Service.Rest.Web.Models.Query;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Navient.Presentation.Service.Rest.Services;

/// <summary>
/// </summary>
public interface IArtificialIntelligenceService
{
  /// <summary>
  /// </summary>
  /// <param name="intents"></param>
  /// <returns></returns>
  Task<IntentsResponse> TestIntents(IntentsRequest intents);

  /// <summary>
  /// </summary>
  /// <param name="sessionId"></param>
  /// <param name="query"></param>
  /// <param name="cid"></param>
  /// <returns></returns>
  Task<QueryResponse> Query(Guid sessionId, string query, int cid);

  /// <summary>
  /// </summary>
  /// <param name="intents"></param>
  /// <returns></returns>
  Task<string> DeployIntents(IntentsRequest intents);
}

/// <summary>
/// </summary>
public class ArtificialIntelligenceService : IArtificialIntelligenceService
{
  //private readonly IConfiguration _configuration;
  private readonly IAuthorizationService _authService;
  private readonly HttpClient _httpClient; //TODO:  convert to httpClientFactory
  private readonly ILogger _logger;
  private readonly IChatTranscriptService _logService;

  private const string _IA_DEPLOY = "/ia_deploy";
  private const string _IA_TEST = "/ia_test";
  private const string _JSON = "application/json";
  private const string _LOG_QUERY_ERROR = "Error querying AI:";
  private const string _LOG_QUERY_SUCCESS = "Successfully queried AI";
  private const string _LOG_TEST_INTENTS_ERROR = "Error testing intents:";
  private const string _LOG_TEST_INTENTS_SUCCESS = "Successfully tested intents";
  private const string _QUERY = "/qna?query={0}&uid={1}&cid={2}";

  /// <summary>
  /// </summary>
  /// <param name="configuration"></param>
  /// <param name="logService"></param>
  /// <param name="authService"></param>
  /// <param name="logger"></param>
  public ArtificialIntelligenceService(IConfiguration configuration, IChatTranscriptService logService, IAuthorizationService authService,
      ILogger<ArtificialIntelligenceService> logger)
  {
    //_configuration = configuration;
    _logService = logService;
    _authService = authService;

    var uri = configuration.GetSection("AIService").GetValue("APIUrl", "");
    _httpClient = new HttpClient
    {
      BaseAddress = new Uri(uri)
    };
    _logger = logger;
  }

  /// <summary>
  /// </summary>
  /// <param name="intents"></param>
  /// <returns></returns>
  /// <exception cref="HttpRequestException"></exception>
  public async Task<string> DeployIntents(IntentsRequest intents)
  {
    // var response = await _httpClient.PostAsJsonAsync(_Ia_Deploy, intents);

    var json = JsonSerializer.Serialize(intents);
    var content = new StringContent(json, Encoding.UTF8, _JSON);
    var response = await _httpClient.PostAsync(_IA_DEPLOY, content);

    if (response.IsSuccessStatusCode)
    {
      var responseString = await response.Content.ReadAsStringAsync();
      return responseString;
    }

    _logService.LogError(Guid.Empty, ComponentTypes.AI, response);
    throw new HttpRequestException(response.ReasonPhrase);
  }

  /// <summary>
  /// </summary>
  /// <param name="sessionId"></param>
  /// <param name="query"></param>
  /// <param name="cid"></param>
  /// <returns></returns>
  /// <exception cref="HttpRequestException"></exception>
  public async Task<QueryResponse> Query(Guid sessionId, string query, int cid)
  {
    var url = string.Format(_QUERY, query, _authService.GetUserId(), cid);
    var response = await _httpClient.GetAsync(url);

    if (response.IsSuccessStatusCode)
    {
      _logger.LogInformation(_LOG_QUERY_SUCCESS);
      var responseString = await response.Content.ReadAsStringAsync();
      var queryResponse = JsonSerializer.Deserialize<QueryResponse>(responseString);

      _logService.LogChat(sessionId, ComponentTypes.AgentAssistChatBot, queryResponse!);

      return queryResponse;
    }

    _logger.LogError(_LOG_QUERY_ERROR, response.ReasonPhrase);
    _logService.LogError(sessionId, ComponentTypes.AI, response);
    throw new HttpRequestException(response.ReasonPhrase);
  }

  /// <summary>
  /// </summary>
  /// <param name="intents"></param>
  /// <returns></returns>
  /// <exception cref="HttpRequestException"></exception>
  public async Task<IntentsResponse> TestIntents(IntentsRequest intents)
  {
    // var response = await _httpClient.PostAsJsonAsync(_Ia_Test, intents);

    var json = JsonSerializer.Serialize(intents);
    var content = new StringContent(json, Encoding.UTF8, _JSON);
    var response = await _httpClient.PostAsync(_IA_TEST, content);

    if (response.IsSuccessStatusCode)
    {
      _logger.LogInformation(_LOG_TEST_INTENTS_SUCCESS);
      var responseString = await response.Content.ReadAsStringAsync();
      var responseObject = JsonSerializer.Deserialize<IntentsResponse>(responseString);
      return responseObject;
    }

    _logger.LogError(_LOG_TEST_INTENTS_ERROR, response.ReasonPhrase);
    _logService.LogError(Guid.Empty, ComponentTypes.AI, response);
    throw new HttpRequestException(response.ReasonPhrase);
  }
}