using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
  public class User
  {
    public int UserId { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    public Country Country { get; set; }
    public bool NewsletterSub { get; set; }
    public Currency DisplayCurrency { get; set; }
    public virtual IEnumerable<Account> Accounts { get; set; }
    public virtual IEnumerable<CreditCard> CreditCards { get; set; }
    public virtual IEnumerable<CryptoAccount> CryptoAccounts { get; set; }
    public virtual IEnumerable<Loan> Loans { get; set; }
    public virtual IEnumerable<Asset> Assets { get; set; }
    }
}
