using System;
using System.Collections.Generic;
using System.Text;

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
    public decimal Value { get; set; }
    public CryptoAccountValue CryptoAccountValue { get; set; }
  }
}
