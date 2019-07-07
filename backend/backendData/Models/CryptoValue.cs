using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendData.Models
{
  public class CryptoValue
  {
    public int CryptoValueId { get; set; }
    public DateTime Date { get; set; }    
    public Cryptocurrency Cryptocurrency { get; set; }
    public string QuotedCurrency { get; set; }
    public double Price { get; set; }
    public double NoCoins { get; set; }

    [Required, Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    public CryptoAccountValue CryptoAccountValue { get; set; }
  }
}
