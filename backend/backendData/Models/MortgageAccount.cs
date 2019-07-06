using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
  public class MortgageAccount
  {
    public int MortgageAccountId { get; set; }
    [Required] public string AccountName { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public int TermInMonths { get; set; }
    public double AprRate { get; set; }    
    public bool IsActive { get; set; }
    [Required] public Currency QuotedCurrency { get; set; }
    [Required] public User User { get; set; }
    public Property MortgagedProperty { get; set; }
    public virtual IEnumerable<AccountValue> BalancesOwing { get; set; }
  }
}
