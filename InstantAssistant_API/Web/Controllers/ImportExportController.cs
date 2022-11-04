namespace InstantAssistant.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using InstantAssistant.Api.Web.Models.ImportExport;
using InstantAssistant.Api.Services;

[ApiController]
[Route("InstantAssistant/[controller]")]
public class ImportExportController : ControllerBase
{

  private IClientsInfoService _clientsInfoService;
  private IAuthService _authService;

  public ImportExportController(IClientsInfoService service, IAuthService authService)
  {
    _clientsInfoService = service;
    _authService = authService;
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> Export(int id)
  {
    var authorized = _authService.IsUserAuthorized(id);
    if(!authorized)
    {
      return Unauthorized($"User is not authorized to access client {id}");
    }

    return Ok();
  }
}