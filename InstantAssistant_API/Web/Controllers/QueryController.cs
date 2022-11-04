namespace InstantAssistant.Api.Controllers;

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using InstantAssistant.Api.Web.Models.Query;
using InstantAssistant.Api.Services;

[ApiController]
[Route("InstantAssistant/[controller]")]
public class QueryController : ControllerBase
{

  private IAIService _aiService;
  private IAuthService _authService;

  private readonly Regex ssnRegex = new Regex(@"(?!(\d){3}([- .]*)\1{2}\2\1{4})(?!666|000|9\d{2})(\b\d{3}([- .]*)(?!00)\d{2}\4(?!0{4})\d{4}\b)");
  private readonly Regex ccRegex = new Regex(@"(?:(\d{4}[- .]*)(\d{4}[- .]*)(\d{4}[- .]*)((\d{4}[- .]*)|(\d{3}[- .]*)|(\d{2}[- .]*)))");
  private readonly Regex numRegex = new Regex(@"\d{4,}");

  private readonly string mask = "***";

  public QueryController(IAIService service, IAuthService authService)
  {
    _aiService = service;
    _authService = authService;
  }

  [HttpPost]
  public async Task<IActionResult> Query(QueryRequest request)
  {
    var authorized = _authService.IsAdminAuthorized(request.ClientId) || _authService.IsUserAuthorized(request.ClientId);

    if(!authorized)
    {
      return Unauthorized($"User is not authorized to access client {request.ClientId}");
    }

    request.Query = ssnRegex.Replace(request.Query, mask);
    request.Query = ccRegex.Replace(request.Query, mask);
    request.Query = numRegex.Replace(request.Query, mask);

    var response = await _aiService.Query(request.SessionId, request.Query, request.ClientId);
    
    return Ok(response);
  }
}