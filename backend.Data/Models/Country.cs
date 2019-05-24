using System;
using System.Collections.Generic;
using System.Text;

namespace backendData.Models
{
  public class Country
  {
    public string Iso2Code { get; set; }
    public string Iso3Code { get; set; }
    public string Name { get; set; }
    public string Continent { get; set; }
  }
}
