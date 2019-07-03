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

                    AccountValue newAccountValue = new AccountValue()
                    {
                        Date = (System.DateTime)JToken.FromObject(accountValueArg).SelectToken("date"),
                        Value = (decimal)JToken.FromObject(accountValueArg).SelectToken("value"),
                    };

                    BankAccount bankAccount = new BankAccount();
                    var bankAccountId = JToken.FromObject(accountValueArg).SelectToken("bankAccountId");
                    if (bankAccountId != null)
                    {
                        bankAccount.BankAccountId = (int)bankAccountId;
                        newAccountValue.BankAccount = bankAccount;
                    }

                    CashAccount cashAccount = new CashAccount();
                    var cashAccountId = JToken.FromObject(accountValueArg).SelectToken("cashAccountId");
                    if (cashAccountId != null)
                    {
                        cashAccount.CashAccountId = (int)cashAccountId;
                        newAccountValue.CashAccount = cashAccount;
                    }

                    LoanAccount loanAccount = new LoanAccount();
                    var loanAccountId = JToken.FromObject(accountValueArg).SelectToken("loanAccountId");
                    if (loanAccountId != null)
                    {
                        loanAccount.LoanAccountId = (int)loanAccountId;
                        newAccountValue.LoanAccount = loanAccount;
                    }

                    MortgageAccount mortgageAccount = new MortgageAccount();
                    var mortgageAccountId = JToken.FromObject(accountValueArg).SelectToken("loanAccountId");
                    if (mortgageAccountId != null)
                    {
                        mortgageAccount.MortgageAccountId = (int)mortgageAccountId;
                        newAccountValue.MortgageAccount = mortgageAccount;
                    }

                    return accountValueRepository.Add(newAccountValue);
                });


            Field<AccountValueType>(
                "updateAccountValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AccountValueInputType>> { Name = "accountValue" }),
                resolve: context =>
                {
                    var accountValueArg = context.Arguments["accountValue"];

                    AccountValue newAccountValue = new AccountValue()
                    {
                        AccountValueId = (int)JToken.FromObject(accountValueArg).SelectToken("accountValueId"),
                        Date = (System.DateTime)JToken.FromObject(accountValueArg).SelectToken("date"),
                        Value = (decimal)JToken.FromObject(accountValueArg).SelectToken("value"),
                    };

                    BankAccount bankAccount = new BankAccount();
                    var bankAccountId = JToken.FromObject(accountValueArg).SelectToken("bankAccountId");
                    if (bankAccountId != null)
                    {
                        bankAccount.BankAccountId = (int)bankAccountId;
                        newAccountValue.BankAccount = bankAccount;
                    }

                    CashAccount cashAccount = new CashAccount();
                    var cashAccountId = JToken.FromObject(accountValueArg).SelectToken("cashAccountId");
                    if (cashAccountId != null)
                    {
                        cashAccount.CashAccountId = (int)cashAccountId;
                        newAccountValue.CashAccount = cashAccount;
                    }

                    LoanAccount loanAccount = new LoanAccount();
                    var loanAccountId = JToken.FromObject(accountValueArg).SelectToken("loanAccountId");
                    if (loanAccountId != null)
                    {
                        loanAccount.LoanAccountId = (int)loanAccountId;
                        newAccountValue.LoanAccount = loanAccount;
                    }

                    MortgageAccount mortgageAccount = new MortgageAccount();
                    var mortgageAccountId = JToken.FromObject(accountValueArg).SelectToken("loanAccountId");
                    if (mortgageAccountId != null)
                    {
                        mortgageAccount.MortgageAccountId = (int)mortgageAccountId;
                        newAccountValue.MortgageAccount = mortgageAccount;
                    }

                    return accountValueRepository.Update(newAccountValue);
                });
        }
    }
}
