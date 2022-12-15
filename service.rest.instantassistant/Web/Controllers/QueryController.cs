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

namespace Navient.Presentation.Service.Rest.Web.Controllers;

/// <summary>
/// </summary>
[ApiController]
[Route("InstantAssistant/[controller]")]
public class QueryController : ControllerBase
{
  private readonly IArtificialIntelligenceService _aiService;
  private readonly IAuthorizationService _authService;
  private readonly Regex _ccRegex = new(@"(?:(\d{4}[- .]*)(\d{4}[- .]*)(\d{4}[- .]*)((\d{4}[- .]*)|(\d{3}[- .]*)|(\d{2}[- .]*)))");
  private readonly Regex _numRegex = new(@"\d{4,}");
  private readonly Regex _ssnRegex = new(@"(?!(\d){3}([- .]*)\1{2}\2\1{4})(?!666|000|9\d{2})(\b\d{3}([- .]*)(?!00)\d{2}\4(?!0{4})\d{4}\b)");

  private const string _MASK = "***";

  /// <summary>
  /// </summary>
  /// <param name="service"></param>
  /// <param name="authService"></param>
  public QueryController(IArtificialIntelligenceService service, IAuthorizationService authService)
  {
    _aiService = service;
    _authService = authService;
  }

  /// <summary>
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  [HttpPost]
  public async Task<IActionResult> Query(QueryRequest request)
  {
    var authorized = _authService.IsAdminAuthorized(request.ClientId) || _authService.IsUserAuthorized(request.ClientId);

    if (!authorized) return Unauthorized($"User is not authorized to access client {request.ClientId}");

    request.Query = _ssnRegex.Replace(request.Query, _MASK);
    request.Query = _ccRegex.Replace(request.Query, _MASK);
    request.Query = _numRegex.Replace(request.Query, _MASK);

    var response = await _aiService.Query(request.SessionId, request.Query, request.ClientId);

    return Ok(response);
  }
}