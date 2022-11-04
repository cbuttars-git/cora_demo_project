namespace InstantAssistant.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using InstantAssistant.Api.Web.Models.Session;
using InstantAssistant.Api.Services;

[ApiController]
[Route("InstantAssistant/[controller]")]
public class SessionController : ControllerBase
{
  protected readonly IConfiguration Configuration;
  protected readonly String AdminGroup;
  private ISessionService _sessionService;
  private IAuthService _authService;

  public SessionController(IConfiguration configuration, ISessionService sessionService, IAuthService authService)
  {
    Configuration = configuration;
    AdminGroup = Configuration.GetValue<String>("InstantAssistant:AdminGroup");

    _sessionService = sessionService;
    _authService = authService;
  }

  [HttpGet]
  [Route("/InstantAssistant/Admin/IsAdmin")]
  public IActionResult IsFullAdmin()
  {
    var authorized = _authService.IsAuthorized(AdminGroup);
    return Ok(authorized);
  }

  [HttpPost]
  public IActionResult NewSession(SessionRequest request)
  {
    var authorized = _authService.IsAdminAuthorized(request.ClientName) || _authService.IsUserAuthorized(request.ClientName);

    if(!authorized)
    {
      return Unauthorized($"User is not authorized to access {request.ClientName}");
    }

    var response = _sessionService.NewSession(request.ClientName);
    return Ok(response);
  }
}