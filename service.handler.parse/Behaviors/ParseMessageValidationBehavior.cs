// 
//    Project:  handler.parse
//    Created:  09-2022-11
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

namespace Navient.Presentation.Service.Handler.Behaviors;

/// <summary>
/// </summary>
public class ParseMessageValidationBehavior : IPipelineBehavior<ParseMessageCommand, ErrorOr<string>>

{
  private readonly ILogger _logger;
  private readonly IValidator<IParseMessageCommand> _validator;

  private const string _CLASS_NAME = nameof(ParseMessageValidationBehavior);

  /// <summary>
  /// </summary>
  /// <param name="logger"></param>
  /// <param name="validator"></param>
  public ParseMessageValidationBehavior(ILogger logger, IValidator<IParseMessageCommand> validator)
  {
    _logger = logger?.ForContext<ParseMessageValidationBehavior>().LogEnteringConstructor(_CLASS_NAME);
    _validator = validator;
    _logger.LogLeavingConstructor(_CLASS_NAME);
  }

  /// <summary>
  /// </summary>
  /// <param name="request"></param>
  /// <param name="next"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public async Task<ErrorOr<string>> Handle(ParseMessageCommand request, RequestHandlerDelegate<ErrorOr<string>> next, CancellationToken cancellationToken)
  {
    _logger?.LogEnteringMethod(_CLASS_NAME, nameof(Handle));

    if (_validator is null)
      return await next();

    var context = new ValidationContext<IParseMessageCommand>(request);
    var validationResults = await _validator.ValidateAsync(context, cancellationToken).ConfigureAwait(false);
    if (validationResults.IsValid)
      return await next();

    return validationResults.Errors
        .Where(result => result is not null)
        .Select(result => Error.Validation(result.PropertyName, result.ErrorMessage))
        .ToList();
  }
}