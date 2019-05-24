using System;
using System.Collections.Generic;
using System.Text;

namespace backendData.Models
{
  public class PropertyValue
  {
    public int PropertyValueId { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public string Source { get; set; }
    public int PropertyId { get; set; }
  }
}
