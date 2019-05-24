using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
  public class User
  {
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    public string Country { get; set; }
    public bool NewsletterSub { get; set; }
    public Currency DisplayCurrency { get; set; }
    public virtual IEnumerable<BankAccount> BankAccounts { get; set; }
    public virtual IEnumerable<CashAccount> CashAccounts { get; set; }
    public virtual IEnumerable<CreditCard> CreditCards { get; set; }
    public virtual IEnumerable<CryptoAccount> CryptoAccounts { get; set; }
    public virtual IEnumerable<LoanAccount> LoanAccounts { get; set; }
    public virtual IEnumerable<MortgageAccount> MortgageAccounts { get; set; }
    public virtual IEnumerable<Property> Properties { get; set; }
  }
}
