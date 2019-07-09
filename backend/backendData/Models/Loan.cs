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
    [Column(TypeName = "decimal(18,2)")]
    public decimal StartPrincipal { get; set; }
    public DateTime StartDate { get; set; }
    public int TotalTerm { get; set; }      // The total no. of repayment periods
    public int FixedTerm { get; set; }      // The no. of fixed rate repayment periods at the start of the loan
    public string RateType { get; set; }
    public double AprRate { get; set; }    
    public string RepaymentFrequency { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal RepaymentAmount { get; set; } 
    public bool IsActive { get; set; }    
    [Required] public Currency QuotedCurrency { get; set; }
    [Required] public User User { get; set; }
    public Asset SecuredAsset { get; set; }
    public virtual IEnumerable<LoanValue> LoanValues { get; set; }
  }
}