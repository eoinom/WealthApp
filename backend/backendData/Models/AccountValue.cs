using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendData.Models
{
    public class AccountValue
    {
        public int AccountValueId { get; set; }
        [Required] public DateTime Date { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
                
        public double RateToUserCurrency { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValueUserCurrency { get; set; }

        public Account Account { get; set; }
    }
}
