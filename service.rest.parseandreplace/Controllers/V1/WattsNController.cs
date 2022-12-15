// 
//    Project:  rest.parseandreplace
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

using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Navient.Common.Model;
using Swashbuckle.AspNetCore.Annotations;
using static Navient.Presentation.Service.Rest.Constant.MediaType;
using static Navient.Presentation.Service.Rest.Constant.SwaggerAnnotations.WattsNController;

namespace Navient.Presentation.Service.Rest.Controllers.V1;

/// <summary>
/// </summary>
[ApiVersion(@"1.0")]
[Route(@"api/[Controller]")] //required for default version
[Route(@"api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class WattsNController : Controller
{
  private readonly ILogger _logger;
  private readonly IMediator _mediator;
  private const string _CLASS_NAME = nameof(WattsNController);

  /// <summary>
  /// </summary>
  /// <param name="logger"></param>
  /// <param name="mediator"></param>
  public WattsNController(IMediator mediator, ILogger logger)
  {
    _logger = logger.NotNullOrThrowArgumentNullException()
        .ForContext<WattsNController>()
        .LogEnteringConstructor(_CLASS_NAME);
    _mediator = mediator.NotNullOrThrowArgumentNullException(nameof(Mediator));
    _logger.LogLeavingConstructor(_CLASS_NAME);
  }

  /// <summary>
  ///   Retrieves a specific product by unique id
  /// </summary>
  /// <param name="request">
  ///   A Json object.
  ///   <example><![CDATA[
  /// <div>Example request:</div>
  /// <div><br/>
  /// {<br/>
  ///   "data": "{ "valid": { "Json": { "value": 123 } } }",<br>
  ///   "message": "A ~valid.Json.value~ message."<br/>
  /// }</div>
  /// ]]>
  ///   </example>
  /// </param>
  /// <remarks>Awesomeness!</remarks>
  /// <response code="200">
  ///   Every effort is made to return the message, even if not all message variables were located in the provided JSON.
  ///   <example>
  ///     A 123 message.
  ///   </example>
  /// </response>
  /// <response code="400">
  ///   A Bad Request occurs when variable brackets are not in pairs or variables have invalid characters.<br />
  ///   <example>
  ///     Invalid message: This is an ~invalid.message~ because there is an opening tilde "~" but no closing tilde.<br />
  ///   </example>
  ///   <example>
  ///     Invalid message: This is an ~invalid.message"~ because a quote is not a valid character within a JSON node/key.
  ///   </example>
  /// </response>
  /// <response code="500">Something went really wrong!</response>
  [HttpPost]
  [ProducesResponseType(typeof(string), StatusCodes.Status200OK, TEXT_PLAIN)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ParseAndReplace([FromBody][SwaggerRequestBody(FIND_AND_REPLACE_REQUEST_BODY_DESCRIPTION, Required = true)] ParseMessageRequest request)
  {
    IActionResult returnValue = null;
    var tupleLoggerStopWatch = _logger.LogEnteringMethod(_CLASS_NAME, nameof(ParseAndReplace), () =>
    {
      var result = new Stopwatch();
      result.Start();
      return result;
    });

    var result = await _mediator.Send(new ParseMessageCommand(request));

    if (result.IsError)
      returnValue = BadRequest(result);
    else
      returnValue = Ok(result.Value);

    _logger.LogLeavingMethod(_CLASS_NAME, nameof(ParseAndReplace), tupleLoggerStopWatch.Item2);
    return returnValue;
  }
}