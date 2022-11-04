namespace InstantAssistant.Web.Models;

public class Intent
{
    public string? Name { get; set; }
    public string? Response { get; set; }
    public string? Url { get; set; }
    public bool Active { get; set; }
    public string? AiModelTestUpdateDate { get; set; }
    public string? AiModelDeploy { get; set; }
    public string? AiModelDeployDate { get; set; }
    public UserExample[]? UserExamples { get; set; }
}
