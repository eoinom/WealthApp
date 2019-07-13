using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendData.Models
{
    public class AssetValue
    {
        public int AssetValueId { get; set; }
        [Required] public DateTime Date { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        
        public double RateToUserCurrency { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValueUserCurrency { get; set; }

        public string Source { get; set; }      // i.e. where is the valuation coming from (user-input, government data, another website etc.)
        public Asset Asset { get; set; }
    }
}
