namespace InstantAssistant.Api.Web.Models.Intents;

using System.ComponentModel.DataAnnotations;

public class Results
{
  public string[]? New_Intent { get; set; }
  public int[]? New_Intents_ID { get; set; }
  public string[]? New_Userexample { get; set; }
  public int[]? New_Userexample_ID { get; set; }
  public string[]? Old_Intent { get; set; }
  public int[]? Old_Intent_ID { get; set; }
  public string[]? Old_Userexample { get; set; }
  public int[]? Old_Userexample_ID { get; set; }
  public decimal[]? Similarity_Score { get; set; }
}

public class IntentsResponse
{
  [Required]
  public string message { get; set; }
  public Results? results { get; set; }
}