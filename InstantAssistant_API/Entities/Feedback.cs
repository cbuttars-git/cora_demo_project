namespace InstantAssistant.Api.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Feedback", Schema = "Data")]
public class Feedback
{
  [Key]
  public int ID { get; set; }
  public Guid SessionID { get; set; }
  public string FeedbackLog { get; set; }
  public string FeedbackLogRpt { get; set; }
  public bool isSuccess { get; set; }
}