namespace InstantAssistant.Api.Web.Models.Session;

using System.ComponentModel.DataAnnotations;

public class SessionRequest
{
  [Required]
  public string ClientName { get; set; }
}