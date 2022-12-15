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

using Navient.Presentation.Service.Rest.Web.Models.Session;

namespace Navient.Presentation.Service.Rest.Web.Controllers;

/// <summary>
/// </summary>
[ApiController]
[Route("InstantAssistant/[controller]")]
public class SessionController : ControllerBase
{
  private readonly string _adminGroup;
  private readonly IAuthorizationService _authService;
  private readonly ISessionService _sessionService;

  public SessionController(IConfiguration configuration, ISessionService sessionService, IAuthorizationService authService)
  {
    _adminGroup = configuration.GetValue<string>("InstantAssistant:AdminGroup");

    _sessionService = sessionService;
    _authService = authService;
  }

  /// <summary>
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [Route("/InstantAssistant/Admin/IsAdmin")]
  public IActionResult IsFullAdmin()
  {
    var authorized = _authService.IsAuthorized(_adminGroup);
    return Ok(authorized);
  }

  /// <summary>
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  [HttpPost]
  public IActionResult NewSession(SessionRequest request)
  {
    var authorized = _authService.IsAdminAuthorized(request.ClientName) || _authService.IsUserAuthorized(request.ClientName);

    if (!authorized) return Unauthorized($"User is not authorized to access {request.ClientName}");

    var response = _sessionService.NewSession(request.ClientName);
    return Ok(response);
  }
}