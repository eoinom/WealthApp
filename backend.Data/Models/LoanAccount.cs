using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Data.Models
{
  public class LoanAccount
  {
    public int LoanAccountId { get; set; }
    [Required] public string AccountName { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public int TermInMonths { get; set; }
    public decimal AprRate { get; set; }    
    public bool IsActive { get; set; }
    [Required] public Currency QuotedCurrency { get; set; }
    [Required] public User User { get; set; }
  }
}
