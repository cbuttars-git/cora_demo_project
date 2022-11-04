namespace InstantAssistant.Api.Web.Models.Query;

public class QueryResponse
{
    public string query { get; set; } = "";
    public string url { get; set; } = "";
    public string? answer { get; set; }
    public object? options { get; set; }
    public string? chatbot { get; set; }
    public string image_url { get; set; } = "";
    public decimal similar { get; set; } = 0;
    public decimal similar_q { get; set; } = 0;
    public string? userId { get; set; }
}