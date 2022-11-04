namespace InstantAssistant.Api.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ChatBotLog", Schema = "Data")]
public class ChatBotLogs
{
  [Key]
  public int ID { get; set; }
  public string SessionID { get; set; }
  public string ChatLog { get; set; }
  public string ChatLogRpt { get; set; }
  public int ComponentID { get; set; }
}