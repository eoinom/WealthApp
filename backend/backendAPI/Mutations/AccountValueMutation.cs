using backendAPI.Types;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;
using Newtonsoft.Json.Linq;

namespace backendAPI.Mutations
{
    public class AccountValueMutation : ObjectGraphType
    {
        public AccountValueMutation(IAccountValueRepository accountValueRepository)
        {
            Name = "AddAccountValueMutation";

            Field<AccountValueType>(
                "addAccountValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AccountValueInputType>> { Name = "accountValue" }),
                resolve: context =>
                {
                    //var accountValue = context.GetArgument<AccountValue>("accountValue");
                    var accountValueArg = context.Arguments["accountValue"];

                    BankAccount bankAccount = new BankAccount();
                    int bankAccountId = accountValueArg != null
                        ? (int)JToken.FromObject(accountValueArg).SelectToken("bankAccountId")
                        : 0;
                    bankAccount.BankAccountId = bankAccountId;

                    CashAccount cashAccount = new CashAccount();
                    int cashAccountId = accountValueArg != null
                        ? (int)JToken.FromObject(accountValueArg).SelectToken("cashAccountId")
                        : 0;
                    cashAccount.CashAccountId = cashAccountId;

                    LoanAccount loanAccount = new LoanAccount();
                    int loanAccountId = accountValueArg != null
                        ? (int)JToken.FromObject(accountValueArg).SelectToken("loanAccountId")
                        : 0;
                    loanAccount.LoanAccountId = loanAccountId;

                    MortgageAccount mortgageAccount = new MortgageAccount();
                    int mortgageAccountId = accountValueArg != null
                        ? (int)JToken.FromObject(accountValueArg).SelectToken("mortgageAccountId")
                        : 0;
                    mortgageAccount.MortgageAccountId = mortgageAccountId;

                    AccountValue newAccountValue = new AccountValue()
                    {
                        Date = (System.DateTime)JToken.FromObject(accountValueArg).SelectToken("date"),
                        Value = (decimal)JToken.FromObject(accountValueArg).SelectToken("value"),
                        BankAccount = bankAccount,
                        CashAccount = cashAccount,
                        LoanAccount = loanAccount,
                        MortgageAccount = mortgageAccount
                    };

                    return accountValueRepository.Add(newAccountValue);
                });
        }
    }
}
