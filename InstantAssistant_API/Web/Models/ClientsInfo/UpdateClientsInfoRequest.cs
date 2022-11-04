namespace InstantAssistant.Api.Web.Models.ClientsInfo;

using InstantAssistant.Api.Entities;

public class UpdateClientsInfoRequest
{
  public string ChatBotName { get; set; }
  public IAConfigData IAConfigData { get; set; }
}