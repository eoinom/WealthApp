using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backendData.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        [Required] public string AccountName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public string Institution { get; set; }
        [Required] public Currency QuotedCurrency { get; set; }
        [Required] public User User { get; set; }
        public virtual IEnumerable<AccountValue> AccountValues { get; set; }
    }
}
