using backendData.Models;
using GraphQL.Types;

namespace backendAPI.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.UserId);
            Field(x => x.Email);
            Field(x => x.Password);
            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Country, type: typeof(CountryType));
            Field(x => x.NewsletterSub);
            //Field(x => x.DisplayCurrency);
            //Field(x => x.BankAccounts);
            //Field(x => x.CashAccounts);
            //Field(x => x.CreditCards);
            //Field(x => x.CryptoAccounts);
            //Field(x => x.LoanAccounts);
            //Field(x => x.MortgageAccounts);
            //Field(x => x.Properties);
        }
    }
}
