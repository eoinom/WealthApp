using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
    public class CreditCard
  {
    public int CreditCardId { get; set; }
    [Required] public string CreditCardName { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; }
    public string Institution { get; set; }
    [Required] public Currency QuotedCurrency { get; set; }
    [Required] public User User { get; set; }
    public virtual IEnumerable<CreditCardValue> CreditCardValues { get; set; }
  }
}
