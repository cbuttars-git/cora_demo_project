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

namespace Navient.Presentation.Service.Rest.Web.Controllers;

/// <summary>
/// </summary>
[ApiController]
[Route("InstantAssistant/Ai")]
public class ArtificialIntelligenceController : ControllerBase
{
  private readonly IArtificialIntelligenceService _aiService;
  private readonly IAuthorizationService _authService;

  /// <summary>
  /// </summary>
  /// <param name="service"></param>
  /// <param name="authService"></param>
  public ArtificialIntelligenceController(IArtificialIntelligenceService service, IAuthorizationService authService)
  {
    _aiService = service;
    _authService = authService;
  }

  /// <summary>
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  [HttpPost]
  [Route("Intents/Deploy")]
  public async Task<IActionResult> DeployIntents(IntentsRequest request)
  {
    var authorized = _authService.IsAdminAuthorized(request.ChatBotID);
    if (!authorized) return Unauthorized($"User is not authorized to administer client {request.ChatBotID}");

    var response = await _aiService.DeployIntents(request);
    return Ok(response);
  }

  /// <summary>
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  [HttpPost]
  [Route("Intents/Test")]
  public async Task<IActionResult> TestIntents(IntentsRequest request)
  {
    var authorized = _authService.IsAdminAuthorized(request.ChatBotID);
    if (!authorized) return Unauthorized($"User is not authorized to administer client {request.ChatBotID}");

    var response = await _aiService.TestIntents(request);
    return Ok(response);
  }
}