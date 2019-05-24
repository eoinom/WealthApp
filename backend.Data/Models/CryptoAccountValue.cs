using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendData.Models
{
  public class CryptoAccountValue
  {
    public int CryptoAccountValueId { get; set; }
    [Required] public DateTime Date { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    public CryptoAccount CryptoAccount { get; set; }
    public virtual IEnumerable<CryptoValue> CryptoValues { get; set; }
  }
}
