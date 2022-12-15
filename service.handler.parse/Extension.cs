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

using Navient.Presentation.Service.Handler.Behaviors;
using Navient.Presentation.Service.Handler.Validations;

namespace Navient.Presentation.Service.Handler;

/// <summary>
/// </summary>
public static class Extension
{
  /// <summary>
  /// </summary>
  /// <param name="parameter"></param>
  /// <returns></returns>
  public static IServiceCollection AddParseMessage(this IServiceCollection parameter)
  {
    parameter.AddMediatR(typeof(ParseMessageCommandHandler));
    parameter.AddScoped<IValidator<IParseMessageCommand>, ParseCommandValidator>();
    parameter.AddScoped<IRequestHandler<ParseMessageCommand, ErrorOr<string>>, ParseMessageCommandHandler>();
    parameter.AddTransient<IPipelineBehavior<ParseMessageCommand, ErrorOr<string>>, ParseMessageValidationBehavior>();
    parameter.AddTransient<IPipelineBehavior<ParseMessageCommand, ErrorOr<string>>, ParseMessageCommandBehavior>();
    return parameter;
  }

  internal static bool IsValidTokenPairs(this IList<int> parameter)
  {
    return parameter?.Any() is true && parameter.Count() % 2 == 0;
  }

  internal static IList<char> GetMessageAsArrayOfCharacters(this string parameter)
  {
    return string.IsNullOrEmpty(parameter) ? null : parameter.ToCharArray().ToList();
  }

  internal static IList<int> GetBracketPositions(this IList<char> parameter, char bracket = '~')
  {
    if (parameter?.Any() is not true) return null;
    var bracketCount = 0;
    var result = new List<int>();

    for (var i = 0; i < parameter.Count; i++)
      if (parameter[i].Equals(bracket))
      {
        bracketCount++;
        result.Add(bracketCount % 2 == 0 ? i : i + 1);
      }


    return result.Any() ? result : null;
  }

  internal static IList<KeyValuePair<string, string>> GetValuesFromJson(this string parameter, List<string> jsonTokens)
  {
    if (string.IsNullOrWhiteSpace(parameter) || jsonTokens?.Any() is not true) return null;

    var result = new List<KeyValuePair<string, string>>(jsonTokens.Count);
    var jsonObject = JObject.Parse(parameter);
    result.AddRange(from token in jsonTokens
                    let jsonToken = jsonObject.SelectToken(token)?.Type
                    let value = jsonToken == JTokenType.Date ? jsonObject.SelectToken(token)?.ToObject<DateTime?>()?.ToString("D") : jsonObject.SelectToken(token)?.ToObject<string>()
                    where !string.IsNullOrWhiteSpace(value)
                    select new KeyValuePair<string, string>($"~{token}~", value));

    return result;
  }

  internal static IList<TokenPartition> GetTokenPartitions(this IList<int> parameter)
  {
    if (parameter?.Any() is not true) return null;

    var result = new List<TokenPartition>();

    for (var i = 0; i < parameter.Count - 1; i++)
    {
      result.Add(new TokenPartition { EndIndex = parameter[i + 1], StartIndex = parameter[i] });
      ++i;
    }

    return result;
  }

  internal static List<string> GetTokensFromMessage(this string message, IList<TokenPartition> tokenPartitions)
  {
    if (string.IsNullOrEmpty(message) || tokenPartitions?.Any() is not true) return null;
    var messageLength = message.Length;

    return (from tokenPartition in tokenPartitions
            where messageLength > tokenPartition.StartIndex && messageLength >= tokenPartition.EndIndex && tokenPartition.EndIndex >= tokenPartition.StartIndex
            select message.Substring(tokenPartition.StartIndex, tokenPartition.EndIndex - tokenPartition.StartIndex)).ToList();
  }

  internal static string ReplaceMessageTokens(this string parameter, IList<KeyValuePair<string, string>> tokenPairs)
  {
    if (string.IsNullOrEmpty(parameter) || tokenPairs?.Any() is not true) return parameter;

    return tokenPairs.Aggregate(parameter, (current, token) => current.Replace(token.Key, !string.IsNullOrWhiteSpace(token.Value) ? token.Value : token.Key));
  }
}