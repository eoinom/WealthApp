using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendData.Models
{
  public class CreditCardValue
    {
    public int CreditCardValueId { get; set; }
    [Required] public DateTime Date { get; set; }

    [Required, Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    public CreditCard CreditCard { get; set; }
  }
}
