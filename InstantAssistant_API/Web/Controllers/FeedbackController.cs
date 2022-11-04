namespace InstantAssistant.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using InstantAssistant.Api.Web.Models.Feedback;
using InstantAssistant.Api.Services;

[ApiController]
[Route("InstantAssistant/[controller]")]
public class FeedbackController : ControllerBase
{
  protected readonly IConfiguration Configuration;
  protected readonly String AdminGroup;
  private IFeedbackService _feedbackService;
  private IAuthService _authService;

  public FeedbackController(IConfiguration configuration, IFeedbackService service, IAuthService authService)
  {
    Configuration = configuration;
    AdminGroup = Configuration.GetValue<String>("InstantAssistant:AdminGroup");

    _feedbackService = service;
    _authService = authService;
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    var authorized = _authService.IsAuthorized(AdminGroup);
    if(!authorized)
    {
      return Unauthorized("You do not have permission to access feedback logs.");
    }

    var feedback = _feedbackService.GetAll();
    return Ok(feedback);
  }

  [HttpGet("{session}")]
  public IActionResult GetAllBySession(Guid session)
  {
    var authorized = _authService.IsAuthorized(AdminGroup);
    if(!authorized)
    {
      return Unauthorized("You do not have permission to access feedback logs.");
    }

    var feedback = _feedbackService.GetAllBySession(session);
    return Ok(feedback);
  }

  [HttpPost]
  public IActionResult Create(FeedbackRequest request)
  {
    var authorized = _authService.IsUserAuthorized(request.ClientId);
    if(!authorized)
    {
      return Unauthorized($"User is not authorized to access client {request.ClientId}");
    }

    try
    {
      var id = _feedbackService.Create(request);
      return Ok(id);
    }
    catch (NotAuthorizedException ex)
    {
      return Unauthorized(ex.Message);
    }
    
  }
}