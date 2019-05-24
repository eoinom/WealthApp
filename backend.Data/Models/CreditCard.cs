using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Data.Models
{
  public class CreditCard
  {
    public int CreditCardId { get; set; }
    [Required] public string CreditCardName { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; }
    [Required] public Currency QuotedCurrency { get; set; }
    [Required] public User User { get; set; }
    public virtual IEnumerable<AccountValue> BalancesOwing { get; set; }
  }
}
