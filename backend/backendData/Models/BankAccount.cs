using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
  public class BankAccount
  {
    public int BankAccountId { get; set; }
    [Required] public string AccountName { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; }
    [Required] public string Institution { get; set; }
    [Required] public string AccountType { get; set; }
    [Required] public Currency QuotedCurrency { get; set; }
    [Required] public User User { get; set; }
    public virtual IEnumerable<AccountValue> AccountValues { get; set; }    
  }
}
