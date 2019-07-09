using backendAPI.Types;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;
using Newtonsoft.Json.Linq;

namespace backendAPI.Mutations
{
    public class AccountMutation : ObjectGraphType
    {
        public AccountMutation(IAccountRepository accountRepository)
        {
            Name = "AccountMutations";

            Field<LoanType>(
                "addAccount",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AccountInputType>> { Name = "account" }),
                resolve: context =>
                {
                    //var account = context.GetArgument<Account>("account");
                    var accountArg = context.Arguments["account"];
                    
                    Currency accountCurrency = new Currency();
                    var currencyCode = accountArg != null
                        ? (string)JToken.FromObject(accountArg).SelectToken("quotedCurrency")
                        : null;
                    accountCurrency.Code = currencyCode;

                    User accountUser = new User();
                    int userId = accountArg != null
                        ? (int)JToken.FromObject(accountArg).SelectToken("userId")
                        : 0;
                    accountUser.UserId = userId;

                    Account newAccount = new Account()
                    {                        
                        AccountName = (string)JToken.FromObject(accountArg).SelectToken("accountName"),
                        Description = (string)JToken.FromObject(accountArg).SelectToken("description"),
                        Type = (string)JToken.FromObject(accountArg).SelectToken("type"),
                        IsActive = (bool)JToken.FromObject(accountArg).SelectToken("isActive"),
                        Institution = (string)JToken.FromObject(accountArg).SelectToken("institution"),
                        QuotedCurrency = accountCurrency,
                        User = accountUser
                    };

                    return accountRepository.Add(newAccount);
                });

            Field<LoanType>(
                "updateAccount",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AccountInputType>> { Name = "account" }),
                resolve: context =>
                {
                    var accountArg = context.Arguments["account"];

                    Currency accountCurrency = new Currency();
                    var currencyCode = accountArg != null
                        ? (string)JToken.FromObject(accountArg).SelectToken("quotedCurrency")
                        : null;
                    accountCurrency.Code = currencyCode;

                    User accountUser = new User();
                    int userId = accountArg != null
                        ? (int)JToken.FromObject(accountArg).SelectToken("userId")
                        : 0;
                    accountUser.UserId = userId;

                    Account newAccount = new Account()
                    {
                        AccountId = (int)JToken.FromObject(accountArg).SelectToken("accountId"),
                        AccountName = (string)JToken.FromObject(accountArg).SelectToken("accountName"),
                        Description = (string)JToken.FromObject(accountArg).SelectToken("description"),
                        Type = (string)JToken.FromObject(accountArg).SelectToken("type"),
                        IsActive = (bool)JToken.FromObject(accountArg).SelectToken("isActive"),
                        Institution = (string)JToken.FromObject(accountArg).SelectToken("institution"),
                        QuotedCurrency = accountCurrency,
                        User = accountUser
                    };

                    return accountRepository.Update(newAccount);
                });

            Field<StringGraphType>(
                "deleteAccount",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "accountId" }),
                resolve: context =>
                {
                    int accountId = context.GetArgument<int>("accountId");
                    try
                    {
                        accountRepository.Delete(accountId);
                        return $"The account with the id: {accountId} has been successfully deleted from the database.";
                    }
                    catch (System.Exception ex)
                    {
                        //return ex.ToString();     // for testing purposes
                        return null;
                    }                    
                });

        }
    }
}
