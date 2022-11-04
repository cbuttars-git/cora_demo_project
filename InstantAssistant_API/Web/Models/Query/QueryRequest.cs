namespace InstantAssistant.Api.Web.Models.Query;

using System.ComponentModel.DataAnnotations;

public class QueryRequest
{
  [Required]
  public Guid SessionId { get; set; }
  [Required]
  public string Query { get; set; }
  public int ClientId { get; set; }
}