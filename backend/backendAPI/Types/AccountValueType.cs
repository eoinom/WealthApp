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
            Field(x => x.BankAccount, nullable: false, type: typeof(BankAccountType)).Description("The bank account which the account value applies to.");
            //Field(x => x.CashAccount, nullable: false, type: typeof(CashAccountType)).Description("The cash account which the account value applies to.");
            //Field(x => x.LoanAccount, nullable: false, type: typeof(LoanAccountType)).Description("The loan account which the account value applies to.");
            //Field(x => x.MortgageAccount, nullable: false, type: typeof(MortgageAccountType)).Description("The mortgage account which the account value applies to.");
        }
    }
}