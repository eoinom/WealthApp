using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendData.Models
{
  public class AccountValue
  {
    public int AccountValueId { get; set; }
    [Required] public DateTime Date { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    public BankAccount BankAccount { get; set; }
    public CashAccount CashAccount { get; set; }
    public LoanAccount LoanAccount { get; set; }
    public MortgageAccount MortgageAccount { get; set; }
  }
}
