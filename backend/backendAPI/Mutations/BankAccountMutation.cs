using backendAPI.Types;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;
using Newtonsoft.Json.Linq;

namespace backendAPI.Mutations
{
    public class BankAccountMutation : ObjectGraphType
    {
        public BankAccountMutation(IBankAccountRepository bankAccountRepository)
        {
            Name = "BankAccountMutations";

            Field<BankAccountType>(
                "addBankAccount",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BankAccountInputType>> { Name = "bankAccount" }),
                resolve: context =>
                {
                    //var bankAccount = context.GetArgument<BankAccount>("bankAccount");
                    var bankAccountArg = context.Arguments["bankAccount"];
                    
                    Currency bankAccountCurrency = new Currency();
                    var currencyCode = bankAccountArg != null
                        ? (string)JToken.FromObject(bankAccountArg).SelectToken("quotedCurrency")
                        : null;
                    bankAccountCurrency.Code = currencyCode;

                    User bankAccountUser = new User();
                    int userId = bankAccountArg != null
                        ? (int)JToken.FromObject(bankAccountArg).SelectToken("userId")
                        : 0;
                    bankAccountUser.UserId = userId;

                    BankAccount newBankAccount = new BankAccount()
                    {                        
                        AccountName = (string)JToken.FromObject(bankAccountArg).SelectToken("accountName"),
                        Description = (string)JToken.FromObject(bankAccountArg).SelectToken("description"),
                        Type = (string)JToken.FromObject(bankAccountArg).SelectToken("type"),
                        IsActive = (bool)JToken.FromObject(bankAccountArg).SelectToken("isActive"),
                        Institution = (string)JToken.FromObject(bankAccountArg).SelectToken("institution"),
                        QuotedCurrency = bankAccountCurrency,
                        User = bankAccountUser
                    };

                    return bankAccountRepository.Add(newBankAccount);
                });

            Field<BankAccountType>(
                "updateBankAccount",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BankAccountInputType>> { Name = "bankAccount" }),
                resolve: context =>
                {
                    var bankAccountArg = context.Arguments["bankAccount"];

                    Currency bankAccountCurrency = new Currency();
                    var currencyCode = bankAccountArg != null
                        ? (string)JToken.FromObject(bankAccountArg).SelectToken("quotedCurrency")
                        : null;
                    bankAccountCurrency.Code = currencyCode;

                    User bankAccountUser = new User();
                    int userId = bankAccountArg != null
                        ? (int)JToken.FromObject(bankAccountArg).SelectToken("userId")
                        : 0;
                    bankAccountUser.UserId = userId;

                    BankAccount newBankAccount = new BankAccount()
                    {
                        BankAccountId = (int)JToken.FromObject(bankAccountArg).SelectToken("bankAccountId"),
                        AccountName = (string)JToken.FromObject(bankAccountArg).SelectToken("accountName"),
                        Description = (string)JToken.FromObject(bankAccountArg).SelectToken("description"),
                        Type = (string)JToken.FromObject(bankAccountArg).SelectToken("type"),
                        IsActive = (bool)JToken.FromObject(bankAccountArg).SelectToken("isActive"),
                        Institution = (string)JToken.FromObject(bankAccountArg).SelectToken("institution"),
                        QuotedCurrency = bankAccountCurrency,
                        User = bankAccountUser
                    };

                    return bankAccountRepository.Update(newBankAccount);
                });

            Field<StringGraphType>(
                "deleteBankAccount",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "bankAccountId" }),
                resolve: context =>
                {
                    int bankAccountId = context.GetArgument<int>("bankAccountId");
                    try
                    {
                        bankAccountRepository.Delete(bankAccountId);
                        return $"The bank account with the id: {bankAccountId} has been successfully deleted from the database.";
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
