using backendData.Models;
using GraphQL.Types;

namespace backendAPI.Types
{
    public class AccountValueType : ObjectGraphType<AccountValue>
    {
        public AccountValueType()
        {
            Field(x => x.AccountValueId, nullable: false);
            Field(x => x.Date, nullable: false).Description("The date the account value relates to");
            Field(x => x.Value, nullable: true).Description("The value in the account's quoted currency");
            Field(x => x.RateToUserCurrency, nullable: true).Description("The exchange rate between the account's quoted currency and the user's display currency");
            Field(x => x.ValueUserCurrency, nullable: true).Description("The value in the user's display currency");
            Field(x => x.Account, nullable: false, type: typeof(AccountType)).Description("The account which the account value applies to.");
        }
    }
}