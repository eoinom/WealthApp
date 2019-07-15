using backendAPI.ExternalAPIs;
using backendAPI.Types;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;


namespace backendAPI.Mutations
{
    public class AccountValueMutation : ObjectGraphType
    {

        public AccountValueMutation(IAccountValueRepository accountValueRepository, IAccountRepository accountRepository, IUserRepository userRepository)
        {
            Name = "AccountValueMutations";

            Field<AccountValueType>(
                "addAccountValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AccountValueInputType>> { Name = "accountValue" }),
                resolve: context =>
                {
                    //var accountValue = context.GetArgument<AccountValue>("accountValue");
                    var accountValueArg = context.Arguments["accountValue"];

                    AccountValue newAccountValue = new AccountValue() {
                        Date = (System.DateTime)JToken.FromObject(accountValueArg).SelectToken("date"),
                        Value = (decimal)JToken.FromObject(accountValueArg).SelectToken("value")
                    };

                    var accountId = JToken.FromObject(accountValueArg).SelectToken("accountId");
                    if (accountId != null)
                    {
                        Account account = new Account();
                        account.AccountId = (int)accountId;
                        newAccountValue.Account = account;
                    }

                    if (JToken.FromObject(accountValueArg).Contains("rateToUserCurrency"))
                    {
                        newAccountValue.RateToUserCurrency = (double)JToken.FromObject(accountValueArg).SelectToken("rateToUserCurrency");

                        if (JToken.FromObject(accountValueArg).Contains("valueUserCurrency"))
                        {
                            newAccountValue.ValueUserCurrency = (decimal)JToken.FromObject(accountValueArg).SelectToken("valueUserCurrency");
                        }
                        else
                        {
                            newAccountValue.ValueUserCurrency = newAccountValue.Value * (decimal)newAccountValue.RateToUserCurrency;
                        }
                    }
                    else
                    {
                        // get exchange rate from external API
                        Account account = accountRepository.GetById(newAccountValue.Account.AccountId);
                        User user = userRepository.GetById(account.User.UserId);
                        string baseCurrency = account.QuotedCurrency.Code;
                        string toCurrency = user.DisplayCurrency.Code;
                        if (baseCurrency == toCurrency)
                        {
                            newAccountValue.RateToUserCurrency = 1.0;
                            newAccountValue.ValueUserCurrency = newAccountValue.Value;
                        }
                        else
                        {
                            string apiRequest = newAccountValue.Date.ToString("yyyy-MM-dd");
                            if (baseCurrency == "EUR")
                            {
                                apiRequest += "?symbols=" + toCurrency;
                            }
                            else
                            {
                                apiRequest += "?base=" + baseCurrency + "&symbols=" + toCurrency;
                            }

                            var exchangeRates = new ExchangeRates();
                            Task<string> task = Task.Run<string>(async () => await exchangeRates.GetExchangeRate(apiRequest));

                            newAccountValue.RateToUserCurrency = (double)JObject.Parse(task.Result).SelectToken("rates." + toCurrency);
                            newAccountValue.ValueUserCurrency = newAccountValue.Value * (decimal)newAccountValue.RateToUserCurrency;
                        }
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
                        Value = (decimal)JToken.FromObject(accountValueArg).SelectToken("value")
                    };
                    
                    var accountId = JToken.FromObject(accountValueArg).SelectToken("accountId");
                    if (accountId != null)
                    {
                        Account account = new Account();
                        account.AccountId = (int)accountId;
                        newAccountValue.Account = account;
                    }

                    AccountValue accountValueToUpdate = accountValueRepository.GetById(newAccountValue.AccountValueId);

                    if (JToken.FromObject(accountValueArg).Contains("rateToUserCurrency"))
                    {
                        newAccountValue.RateToUserCurrency = (double)JToken.FromObject(accountValueArg).SelectToken("rateToUserCurrency");

                        if (JToken.FromObject(accountValueArg).Contains("valueUserCurrency"))
                        {
                            newAccountValue.ValueUserCurrency = (decimal)JToken.FromObject(accountValueArg).SelectToken("valueUserCurrency");
                        }
                        else
                        {
                            newAccountValue.ValueUserCurrency = newAccountValue.Value * (decimal)newAccountValue.RateToUserCurrency;
                        }
                    }
                    else if (accountValueToUpdate.Date == newAccountValue.Date && accountValueToUpdate.RateToUserCurrency > 0) {
                        newAccountValue.RateToUserCurrency = accountValueToUpdate.RateToUserCurrency;
                        newAccountValue.ValueUserCurrency = newAccountValue.Value * (decimal)newAccountValue.RateToUserCurrency;
                    }
                    else
                    {
                        // get exchange rate from external API
                        Account account = accountRepository.GetById(newAccountValue.Account.AccountId);
                        User user = userRepository.GetById(account.User.UserId);
                        string baseCurrency = account.QuotedCurrency.Code;
                        string toCurrency = user.DisplayCurrency.Code;
                        if (baseCurrency == toCurrency)
                        {
                            newAccountValue.RateToUserCurrency = 1.0;
                            newAccountValue.ValueUserCurrency = newAccountValue.Value;
                        }
                        else
                        {
                            string apiRequest = newAccountValue.Date.ToString("yyyy-MM-dd");
                            if (baseCurrency == "EUR")
                            {
                                apiRequest += "?symbols=" + toCurrency;
                            }
                            else
                            {
                                apiRequest += "?base=" + baseCurrency + "&symbols=" + toCurrency;
                            }

                            var exchangeRates = new ExchangeRates();
                            Task<string> task = Task.Run<string>(async () => await exchangeRates.GetExchangeRate(apiRequest));

                            newAccountValue.RateToUserCurrency = (double)JObject.Parse(task.Result).SelectToken("rates." + toCurrency);
                            newAccountValue.ValueUserCurrency = newAccountValue.Value * (decimal)newAccountValue.RateToUserCurrency;
                        }
                    }

                    return accountValueRepository.Update(newAccountValue);
                });

            Field<StringGraphType>(
                "deleteAccountValueById",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "accountValueId" }),
                resolve: context =>
                {
                    int accountValueId = context.GetArgument<int>("accountValueId");
                    try
                    {
                        accountValueRepository.DeleteById(accountValueId);
                        return $"The account value with the id: {accountValueId} has been successfully deleted from the database.";
                    }
                    catch (System.Exception ex)
                    {
                        //return ex.ToString();     // for testing purposes
                        return null;
                    }
                });

            Field<StringGraphType>(
                "deleteAccountValuesByIds",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<IntGraphType>>> { Name = "accountValueIds" }),
                resolve: context =>
                {
                    int[] accountValueIds = context.GetArgument<int[]>("accountValueIds");
                    try
                    {
                        accountValueRepository.DeleteByIds(accountValueIds);

                        string accountValueIdsStr = string.Join(",", accountValueIds.Select(x => x.ToString()).ToArray());

                        return $"The account values with the ids: [{accountValueIdsStr}] have been successfully deleted from the database.";
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
