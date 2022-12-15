// 
//    Project:  model.wattsn
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

using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using static Navient.Common.Model.Constant;


namespace Navient.Common.Model;

/// <summary>
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class ParseMessageRequest : JsonBase<ParseMessageRequest>, IParseMessageRequest
{
  /// <summary>
  ///   Data property. Expects non-null JSON string/text that contains at a minimum the JSON nodes required to properly format the value for <see cref="Message" />.
  /// </summary>
  /// <example><![CDATA[
  /// {
  ///   "json_data": {
  ///     "some": {
  ///       "value": "JSON Node Variable"
  ///     }
  ///   }
  /// }
  /// ]]>
  /// </example>
  [SwaggerSchema(DataPropertyAnnotation.SWAGGER_ANNOTATION_DESCRIPTION, Title = DataPropertyAnnotation.SWAGGER_ANNOTATION_TITLE,
      Format = DataPropertyAnnotation.SWAGGER_ANNOTATION_FORMAT, Nullable = false)]
  [JsonProperty(JSON_PROPERTY_NAME_DATA)]
  public string Data { get; set; }

  /// <summary>
  ///   Message property. Expects a non-null string/text message that contains tilde (~) bracketed variables that match the JSON string/text value of <see cref="Data" />.
  /// </summary>
  /// <example>
  ///   This is a message with a replacement variable named ~json_data.some.value~.
  /// </example>
  [SwaggerSchema(MessagePropertyAnnotation.SWAGGER_ANNOTATION_DESCRIPTION, Title = MessagePropertyAnnotation.SWAGGER_ANNOTATION_TITLE,
      Format = MessagePropertyAnnotation.SWAGGER_ANNOTATION_FORMAT, Nullable = false)]
  [JsonProperty(JSON_PROPERTY_NAME_MESSAGE)]
  public string Message { get; set; }
}