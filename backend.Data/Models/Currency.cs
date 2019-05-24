using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
  public class Currency
  {
    [Key] public string Code { get; set; }

    public string NameShort { get; set; }
    public string NameLong { get; set; }
  }
}
