using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
    public class Country
    {
        [Key]
        public string Iso2Code { get; set; }
        public string Iso3Code { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public Currency MainCurrency { get; set; }
    }
}
