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

using Navient.Presentation.Service.Rest.Web.Models.Feedback;

namespace Navient.Presentation.Service.Rest.Web.Controllers;

/// <summary>
/// </summary>
[ApiController]
[Route("InstantAssistant/[controller]")]
public class FeedbackController : ControllerBase
{
  private readonly string _adminGroup;
  private readonly IAuthorizationService _authService;
  private readonly IFeedbackService _feedbackService;

  /// <summary>
  /// </summary>
  /// <param name="configuration"></param>
  /// <param name="service"></param>
  /// <param name="authService"></param>
  public FeedbackController(IConfiguration configuration, IFeedbackService service, IAuthorizationService authService)
  {
    _adminGroup = configuration.GetValue<string>("InstantAssistant:AdminGroup");
    _feedbackService = service;
    _authService = authService;
  }

  /// <summary>
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  [HttpPost]
  public IActionResult Create(FeedbackRequest request)
  {
    var authorized = _authService.IsUserAuthorized(request.ClientId);
    if (!authorized) return Unauthorized($"User is not authorized to access client {request.ClientId}");

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

  /// <summary>
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  public IActionResult GetAll()
  {
    var authorized = _authService.IsAuthorized(_adminGroup);
    if (!authorized) return Unauthorized("You do not have permission to access feedback logs.");

    var feedback = _feedbackService.GetAll();
    return Ok(feedback);
  }

  /// <summary>
  /// </summary>
  /// <param name="session"></param>
  /// <returns></returns>
  [HttpGet("{session}")]
  public IActionResult GetAllBySession(Guid session)
  {
    var authorized = _authService.IsAuthorized(_adminGroup);
    if (!authorized) return Unauthorized("You do not have permission to access feedback logs.");

    var feedback = _feedbackService.GetAllBySession(session);
    return Ok(feedback);
  }
}