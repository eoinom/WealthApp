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
            Field(x => x.Country, type: typeof(CountryType), nullable: true);
            Field(x => x.NewsletterSub);
            Field(x => x.DisplayCurrency, type: typeof(CurrencyType));
            Field(x => x.Accounts, type: typeof(ListGraphType<AccountType>));
            //Field(x => x.CreditCards);            
            //Field(x => x.Loans);
            //Field(x => x.CryptoAccounts);
            //Field(x => x.Assets);
        }
    }
}
