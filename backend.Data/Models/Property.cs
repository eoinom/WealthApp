using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Data.Models
{
  public class Property
  {
    public int PropertyId { get; set; }
    [Required] public string PropertyName { get; set; }
    public string Description { get; set; }
    public string PropertyType { get; set; }
    public bool IsActive { get; set; }
    [Required] public Currency QuotedCurrency { get; set; }
    [Required] public User User { get; set; }
    public virtual IEnumerable<PropertyValue> PropertyValues { get; set; }
  }
}
