using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
  public abstract class CashAccount
  {
    public int CashAccountId { get; set; }
    [Required] public string AccountName { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; }
    [Required] public Currency QuotedCurrency { get; set; }
    [Required] public User User { get; set; }
    public virtual IEnumerable<AccountValue> AccountValues { get; set; }
  }
}
