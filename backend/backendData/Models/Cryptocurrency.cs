using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
  public class Cryptocurrency
  {
    [Key] public string Code { get; set; }

    public string Name { get; set; }
    public string LogoUrl { get; set; }
  }
}
