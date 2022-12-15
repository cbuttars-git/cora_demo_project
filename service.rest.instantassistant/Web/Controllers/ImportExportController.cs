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

namespace Navient.Presentation.Service.Rest.Web.Controllers;

/// <summary>
/// </summary>
[ApiController]
[Route("InstantAssistant/[controller]")]
public class ImportExportController : ControllerBase
{
  private readonly IAuthorizationService _authService;

  /// <summary>
  /// </summary>
  /// <param name="authService"></param>
  public ImportExportController(IAuthorizationService authService)
  {
    _authService = authService;
  }

  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id}")]
  public IActionResult Export(int id)
  {
    var authorized = _authService.IsUserAuthorized(id);
    if (!authorized) return Unauthorized($"User is not authorized to access client {id}");

    return Ok();
  }
}