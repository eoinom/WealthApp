using backendData.Models;
using GraphQL.Types;

namespace backendAPI.Types
{
    public class BankAccountType : ObjectGraphType<BankAccount>
    {
        public BankAccountType()
        {
            Field(x => x.BankAccountId, nullable: false);
            Field(x => x.AccountName, nullable: false).Description("The user's name for the account, e.g. main current a/c");
            Field(x => x.Description, nullable: true).Description("The user's description for the account");
            Field(x => x.Type, nullable: true).Description("They type of account, e.g. current, savings etc.");
            Field(x => x.IsActive, nullable: true).Description("True if the account is currently account, i.e. not closed.");
            Field(x => x.Institution, nullable: false).Description("The institution where the account is held, i.e. bank name.");
            Field(x => x.QuotedCurrency, nullable: false, type: typeof(CurrencyType)).Description("The currency which the account values are quoted in.");
            Field(x => x.User, nullable: false, type: typeof(UserType)).Description("The user which owns the account.");
        }
    }
}