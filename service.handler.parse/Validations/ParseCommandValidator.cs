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

using static Navient.Common.Model.Constant;

namespace Navient.Presentation.Service.Handler.Validations;

/// <summary>
/// </summary>
public class ParseCommandValidator : AbstractValidator<IParseMessageCommand>
{
  private const string _CLASS_NAME = nameof(ParseCommandValidator);

  /// <summary>
  /// </summary>
  /// <param name="logger"></param>
  public ParseCommandValidator(ILogger logger)
  {
    logger?.ForContext<ParseCommandValidator>().LogEnteringConstructor(_CLASS_NAME);

    RuleFor(request => request.Message)
        .Cascade(CascadeMode.Stop)
        .NotNull().OverridePropertyName(JSON_PROPERTY_NAME_MESSAGE)
        .NotEmpty().OverridePropertyName(JSON_PROPERTY_NAME_MESSAGE);
    RuleFor(request => request.JsonString)
        .Cascade(CascadeMode.Stop)
        .NotNull().OverridePropertyName(JSON_PROPERTY_NAME_DATA)
        .NotEmpty().OverridePropertyName(JSON_PROPERTY_NAME_DATA);

    logger?.LogLeavingConstructor(_CLASS_NAME);
  }
}