// 
//    Project:  handler.parse
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

namespace Navient.Presentation.Service.Handler;

/// <summary>
/// </summary>
public class ParseMessageCommandHandler : IRequestHandler<ParseMessageCommand, ErrorOr<string>>
{
  private static Task<string> GetMessage(IParseMessageCommand parseMessageCommand)
  {
    return Task.Run(() =>
    {
      var tokenPositions = parseMessageCommand.Message.GetMessageAsArrayOfCharacters().GetBracketPositions().GetTokenPartitions();

      if (tokenPositions?.Any() is not true) return parseMessageCommand.Message;

      var tokens = parseMessageCommand.Message.GetTokensFromMessage(tokenPositions);

      var jsonTokens = parseMessageCommand.JsonString.GetValuesFromJson(tokens);

      return parseMessageCommand.Message.ReplaceMessageTokens(jsonTokens);
    });
  }

  /// <summary>
  /// </summary>
  /// <param name="parseMessageCommand"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public async Task<ErrorOr<string>> Handle(ParseMessageCommand parseMessageCommand, CancellationToken cancellationToken)
  {
    return await GetMessage(parseMessageCommand).WaitAsync(cancellationToken).ConfigureAwait(false);
  }
}