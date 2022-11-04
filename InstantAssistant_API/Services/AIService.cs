namespace InstantAssistant.Api.Services;

using System.Text.Json;
using System.Text;
using System.Net.Http;

using InstantAssistant.Api.Entities;
using InstantAssistant.Api.Web.Models.Intents;
using InstantAssistant.Api.Web.Models.Query;

public interface IAIService
{
    Task<IntentsResponse> TestIntents(IntentsRequest intents);
    Task<string> DeployIntents(IntentsRequest intents);
    Task<QueryResponse> Query(Guid sessionId, string query, int cid);
}

public class AIService : IAIService
{
  protected readonly IConfiguration Configuration;
  private ILogService _logService;
  private IAuthService _authService;
  private readonly HttpClient _httpClient;

  private readonly ILogger _logger;
  private static readonly string _LogTestIntentsSuccess = "Successfully tested intents";
  private static readonly string _LogTestIntentsError = "Error testing intents:";
  private static readonly string _LogQuerySuccess = "Successfully queried AI";
  private static readonly string _LogQueryError = "Error querying AI:";

  private static readonly string _json = "application/json";
  private static readonly string _Ia_Test = "/ia_test";
  private static readonly string _Ia_Deploy = "/ia_deploy";
  //private static readonly string _Query = "/qnaresponse?query={0}&uid={1}&cid={2}";
  private static readonly string _Query = "/qna?query={0}&uid={1}&cid={2}";

    public AIService(IConfiguration configuration, ILogService logService, IAuthService authService, ILogger<AIService> logger)
  {
    Configuration = configuration;
    _logService = logService;
    _authService = authService;

    var uri = Configuration.GetSection("AIService").GetValue<string>("APIUrl", "");
    _httpClient = new HttpClient()
    {
      BaseAddress = new Uri(uri)
    };
    _logger = logger;
  }

  public async Task<IntentsResponse> TestIntents(IntentsRequest intents)
  {
    // var response = await _httpClient.PostAsJsonAsync(_Ia_Test, intents);

    var json = JsonSerializer.Serialize(intents);
    var content = new StringContent(json, Encoding.UTF8, _json);
    var response = await _httpClient.PostAsync(_Ia_Test, content);

    if(response.IsSuccessStatusCode)
    {
      _logger.LogInformation(_LogTestIntentsSuccess);
      var responseString = await response.Content.ReadAsStringAsync();
      var responseObject = JsonSerializer.Deserialize<IntentsResponse>(responseString);
      return responseObject;
    }
    else
    {
      _logger.LogError(_LogTestIntentsError, response.ReasonPhrase);
      _logService.LogError(Guid.Empty, ComponentTypes.AI, response);
      throw new HttpRequestException(response.ReasonPhrase);
    }
  }

  public async Task<string> DeployIntents(IntentsRequest intents)
  {
    // var response = await _httpClient.PostAsJsonAsync(_Ia_Deploy, intents);

    var json = JsonSerializer.Serialize(intents);
    var content = new StringContent(json, Encoding.UTF8, _json);
    var response = await _httpClient.PostAsync(_Ia_Deploy, content);

    if(response.IsSuccessStatusCode)
    {
      var responseString = await response.Content.ReadAsStringAsync();
      return responseString;
    }
    else
    {
      _logService.LogError(Guid.Empty, ComponentTypes.AI, response);
      throw new HttpRequestException(response.ReasonPhrase);
    }
  }

  public async Task<QueryResponse> Query(Guid sessionId, string query, int cid)
  {
    var url = string.Format(_Query, query, _authService.GetUserId(), cid);
    var response = await _httpClient.GetAsync(url);

    if(response.IsSuccessStatusCode)
    {
      _logger.LogInformation(_LogQuerySuccess);
      var responseString = await response.Content.ReadAsStringAsync();
      var queryResponse = JsonSerializer.Deserialize<QueryResponse>(responseString);
        
      _logService.LogChat(sessionId, ComponentTypes.AgentAssistChatBot, queryResponse);

      return queryResponse;
    }
    else
    {
      _logger.LogError(_LogQueryError, response.ReasonPhrase);
      _logService.LogError(sessionId, ComponentTypes.AI, response);
      throw new HttpRequestException(response.ReasonPhrase);
    }
  }
}
