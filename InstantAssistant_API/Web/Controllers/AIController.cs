namespace InstantAssistant.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using InstantAssistant.Api.Web.Models.Intents;
using InstantAssistant.Api.Services;

[ApiController]
[Route("InstantAssistant/[controller]")]
public class AIController : ControllerBase
{

  private IAIService _aiService;
  private IAuthService _authService;

  public AIController(IAIService service, IAuthService authService)
  {
    _aiService = service;
    _authService = authService;
  }

  [HttpPost]
  [Route("Intents/Test")]
  public async Task<IActionResult> TestIntents(IntentsRequest request)
  {
    var authorized = _authService.IsAdminAuthorized(request.ChatBotID);
    if(!authorized)
    {
      return Unauthorized($"User is not authorized to administer client {request.ChatBotID}");
    }

    var response = await _aiService.TestIntents(request);
    return Ok(response);
  }

  [HttpPost]
  [Route("Intents/Deploy")]
  public async Task<IActionResult> DeployIntents(IntentsRequest request)
  {
    var authorized = _authService.IsAdminAuthorized(request.ChatBotID);
    if(!authorized)
    {
      return Unauthorized($"User is not authorized to administer client {request.ChatBotID}");
    }

    var response = await _aiService.DeployIntents(request);
    return Ok(response);
  }
}