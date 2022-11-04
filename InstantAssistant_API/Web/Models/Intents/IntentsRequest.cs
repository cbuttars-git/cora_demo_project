namespace InstantAssistant.Api.Web.Models.Intents;

using System.ComponentModel.DataAnnotations;

using InstantAssistant.Api.Entities;

public class IntentsRequest
{
  [Required]
  public int ChatBotID { get; set; }
  [Required]
  public Intent[] intents { get; set; }
}