namespace InstantAssistant.Api.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("ClientsInfo", Schema = "Config")]
public class ClientsInfo
{
  [Key]
  public int ChatBotID { get; set; }
  public string? ChatBotName { get; set; }
  public IAConfigData? IAConfigData { get; set; }
}

[NotMapped]
public class Client
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Logo { get; set; }
}

[Keyless]
public class IAConfigData {
  public string? description { get; set; }
  public bool active { get; set; }
  public string? userAdGroup { get; set; }
  public string? adminAdGroup { get; set; }
  public string? placeholder { get; set; }
  public DisplayConfig? displayConfig { get; set; }
  public Intent? welcomeIntent { get; set; }
  public Intent? errorIntent { get; set; }
  public Intent[]? intents { get; set; }
  public String? updatedBy { get; set; }
}

[Keyless]
public class DisplayConfig {
  public string? headerBgColor { get; set; }
  public string? headerFgColor { get; set; }
  public string? footerBgColor { get; set; }
  public string? footerFgColor { get; set; }
  public string? dialogBgColor { get; set; }
  public string? dialogFgColor { get; set; }
  public string? linkColor { get; set; }
  public string? logo { get; set; }
}

[Keyless]
public class Intent {
  public int id { get; set; }
  public string? name { get; set; }
  public string? response { get; set; }
  public string? url { get; set; }
  public bool active { get; set; }
  public bool updated { get; set; }
  public string? aIModelTestUpdateDate { get; set; }
  public bool aIModelDeploy { get; set; }
  public string? aIModelDeployDate { get; set; }
  public UserExample[]? userExamples { get; set; }
}

[Keyless]
public class UserExample {
  public int id { get; set; }
  public string? text { get; set; }
  public bool active { get; set; }
  public bool updated { get; set; }
  public string? aIModelTestDate { get; set; }
  public string? aIModelDeployDate { get; set; }
}