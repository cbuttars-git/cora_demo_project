// 
//    Project:  FiservProxyService
//    Created:  01-2022-11
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

using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Navient.Presentation.Service.Rest.Controllers.V1;

/// <summary>
/// </summary>
[ApiVersion(@"1.0")]
[Route(@"api/[Controller]")] //required for default version
[Route(@"api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class FiservController : Controller
{
  private readonly IReadOnlyApplicationSetting _applicationSetting;
  private readonly ILogger _logger;
  private readonly IMediator _mediator;
  private const string _CLASS_NAME = nameof(FiservController);

  /// <summary>
  /// </summary>
  /// <param name="logger"></param>
  /// <param name="mediator"></param>
  /// <param name="applicationSetting"></param>
  public FiservController(IMediator mediator, IReadOnlyApplicationSetting applicationSetting, ILogger logger)
  {
    _logger = logger.NotNullOrThrowArgumentNullException()
        .ForContext<FiservController>()
        .LogEnteringConstructor(_CLASS_NAME);
    _applicationSetting = applicationSetting.NotNullOrThrowArgumentNullException(nameof(applicationSetting));
    _mediator = mediator.NotNullOrThrowArgumentNullException(nameof(Mediator));
    _logger.LogLeavingConstructor(_CLASS_NAME);
  }


  /// <summary>
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet(@"userId/{id:guid}")]
  [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MEDIA_TYPE_APPLICATION_JSON)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> LoadJsonFile(Guid id)
  {
    var tupleLoggerStopWatch = _logger.LogEnteringMethod(_CLASS_NAME, nameof(LoadJsonFile), () =>
    {
      var result = new Stopwatch();
      result.Start();
      return result;
    });
    var result = await _mediator.Send(new FileLoader(_applicationSetting.GetConnectionString(@"JsonFileLocation"), id));


    _logger.LogLeavingMethod(_CLASS_NAME, nameof(LoadJsonFile), tupleLoggerStopWatch.Item2);
    return Ok(result);
  }
}