using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Models
{
  public class CryptoAccountValue
  {
    public int CryptoAccountValueId { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public CryptoAccount CryptoAccount { get; set; }
    public virtual IEnumerable<CryptoValue> CryptoValues { get; set; }
  }
}
