namespace InstantAssistant.Api.Web.Models.ClientsInfo;

using System.ComponentModel.DataAnnotations;

public class CreateClientsInfoRequest
{
  [Required]
  public string ChatBotName { get; set; }
}