using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
  public class Asset
  {
    public int AssetId { get; set; }
    [Required] public string AssetName { get; set; }
    public string Description { get; set; }
    public string AssetType { get; set; }
    public bool IsActive { get; set; }
    [Required] public Currency QuotedCurrency { get; set; }
    [Required] public User User { get; set; }
    public virtual IEnumerable<AssetValue> AssetValues { get; set; }
  }
}
