using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendData.Models
{
  public class Loan
  {
    public int LoanId { get; set; }
    [Required] public string LoanName { get; set; }
    public string Description { get; set; }
    public string Institution { get; set; }
    public string Type { get; set; }    
    [Required, Column(TypeName = "decimal(18,2)")]
    public decimal StartPrincipal { get; set; }
    public DateTime StartDate { get; set; }
    public int TermInMonths { get; set; }
    public int FixedTermInMonths { get; set; }
    public string RateType { get; set; }
    public double AprRate { get; set; }    
    public string RepaymentFrequency { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal RepaymentAmount { get; set; } 
    public bool IsActive { get; set; }    
    [Required] public Currency QuotedCurrency { get; set; }
    [Required] public User User { get; set; }
    public Asset SecuredAsset { get; set; }
    public virtual IEnumerable<AccountValue> BalancesOwing { get; set; }
  }
}