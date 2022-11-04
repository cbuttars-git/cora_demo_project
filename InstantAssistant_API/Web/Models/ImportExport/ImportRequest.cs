namespace InstantAssistant.Api.Web.Models.ImportExport;

using System.ComponentModel.DataAnnotations;

public class ImportRequest
{
  [Required]
  public int ClientId { get; set; }
}