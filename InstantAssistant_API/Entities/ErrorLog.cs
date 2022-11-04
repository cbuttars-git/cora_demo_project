namespace InstantAssistant.Api.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ErrorLog", Schema = "Data")]
public class ErrorLogs
{
  [Key]
  public int ID { get; set; }
  public string SessionID { get; set; }
  public string ErrorLog { get; set; }
  public int ComponentID { get; set; }
}