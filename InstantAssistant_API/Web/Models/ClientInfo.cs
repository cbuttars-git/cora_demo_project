namespace InstantAssistant.Web.Models;

public class ClientInfo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? UrlSlug { get; set; }
    public bool Active { get; set; }
    public string? AiAdGroup { get; set; }
    public string? InstantAssistantAdGroup { get; set; }
    public DisplayConfig? DisplayConfig { get; set; }
    public Intent[]? Intents { get; set; }
}
