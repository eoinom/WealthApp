using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendData.Models
{
  public class LoanValue
  {
    public int LoanValueId { get; set; }
    [Required] public DateTime Date { get; set; }

    [Required, Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    public Loan Loan { get; set; }
  }
}
