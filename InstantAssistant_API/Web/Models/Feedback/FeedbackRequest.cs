namespace InstantAssistant.Api.Web.Models.Feedback;

using System.ComponentModel.DataAnnotations;

public class FeedbackRequest
{
  [Required]
  public int ClientId { get; set; }
  [Required]
  public Guid SessionId { get; set; }
  public bool Success { get; set; }
  public string Response { get; set; }
  public int FeedbackId { get; set; }
}