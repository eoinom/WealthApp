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
    public class LoanValueMutation : ObjectGraphType
    {
        public LoanValueMutation(ILoanValueRepository loanValueRepository, ILoanRepository loanRepository, IUserRepository userRepository)
        {
            Name = "LoanValueMutations";

            Field<LoanValueType>(
                "addLoanValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoanValueInputType>> { Name = "loanValue" }),
                resolve: context =>
                {
                    //var loanValue = context.GetArgument<LoanValue>("loanValue");
                    var loanValueArg = context.Arguments["loanValue"];

                    LoanValue newLoanValue = new LoanValue() {
                        Date = (System.DateTime)JToken.FromObject(loanValueArg).SelectToken("date"),
                        Value = (decimal)JToken.FromObject(loanValueArg).SelectToken("value")
                    };
                   
                    var loanId = JToken.FromObject(loanValueArg).SelectToken("loanId");
                    if (loanId != null)
                    {
                        Loan loan = new Loan();
                        loan.LoanId = (int)loanId;
                        newLoanValue.Loan = loan;
                    }

                    if (JToken.FromObject(loanValueArg).Contains("rateToUserCurrency"))
                    {
                        newLoanValue.RateToUserCurrency = (double)JToken.FromObject(loanValueArg).SelectToken("rateToUserCurrency");

                        if (JToken.FromObject(loanValueArg).Contains("valueUserCurrency"))
                        {
                            newLoanValue.ValueUserCurrency = (decimal)JToken.FromObject(loanValueArg).SelectToken("valueUserCurrency");
                        }
                        else
                        {
                            newLoanValue.ValueUserCurrency = newLoanValue.Value * (decimal)newLoanValue.RateToUserCurrency;
                        }
                    }
                    else
                    {
                        // get exchange rate from external API
                        Loan loan = loanRepository.GetById(newLoanValue.Loan.LoanId);
                        User user = userRepository.GetById(loan.User.UserId);
                        string baseCurrency = loan.QuotedCurrency.Code;
                        string toCurrency = user.DisplayCurrency.Code;
                        if (baseCurrency == toCurrency)
                        {
                            newLoanValue.RateToUserCurrency = 1.0;
                            newLoanValue.ValueUserCurrency = newLoanValue.Value;
                        }
                        else
                        {
                            string apiRequest = newLoanValue.Date.ToString("yyyy-MM-dd");
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

                            newLoanValue.RateToUserCurrency = (double)JObject.Parse(task.Result).SelectToken("rates." + toCurrency);
                            newLoanValue.ValueUserCurrency = newLoanValue.Value * (decimal)newLoanValue.RateToUserCurrency;
                        }
                    }

                    return loanValueRepository.Add(newLoanValue);
                });


            Field<LoanValueType>(
                "updateLoanValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoanValueInputType>> { Name = "loanValue" }),
                resolve: context =>
                {
                    var loanValueArg = context.Arguments["loanValue"];

                    LoanValue newLoanValue = new LoanValue()
                    {
                        LoanValueId = (int)JToken.FromObject(loanValueArg).SelectToken("loanValueId"),
                        Date = (System.DateTime)JToken.FromObject(loanValueArg).SelectToken("date"),
                        Value = (decimal)JToken.FromObject(loanValueArg).SelectToken("value")
                    };
                    
                    var loanId = JToken.FromObject(loanValueArg).SelectToken("loanId");
                    if (loanId != null)
                    {
                        Loan loan = new Loan();
                        loan.LoanId = (int)loanId;
                        newLoanValue.Loan = loan;
                    }

                    LoanValue loanValueToUpdate = loanValueRepository.GetById(newLoanValue.LoanValueId);

                    if (JToken.FromObject(loanValueArg).Contains("rateToUserCurrency"))
                    {
                        newLoanValue.RateToUserCurrency = (double)JToken.FromObject(loanValueArg).SelectToken("rateToUserCurrency");

                        if (JToken.FromObject(loanValueArg).Contains("valueUserCurrency"))
                        {
                            newLoanValue.ValueUserCurrency = (decimal)JToken.FromObject(loanValueArg).SelectToken("valueUserCurrency");
                        }
                        else
                        {
                            newLoanValue.ValueUserCurrency = newLoanValue.Value * (decimal)newLoanValue.RateToUserCurrency;
                        }
                    }
                    else if (loanValueToUpdate.Date == newLoanValue.Date && loanValueToUpdate.RateToUserCurrency > 0)
                    {
                        newLoanValue.RateToUserCurrency = loanValueToUpdate.RateToUserCurrency;
                        newLoanValue.ValueUserCurrency = newLoanValue.Value * (decimal)newLoanValue.RateToUserCurrency;
                    }
                    else
                    {
                        // get exchange rate from external API
                        Loan loan = loanRepository.GetById(newLoanValue.Loan.LoanId);
                        User user = userRepository.GetById(loan.User.UserId);
                        string baseCurrency = loan.QuotedCurrency.Code;
                        string toCurrency = user.DisplayCurrency.Code;
                        if (baseCurrency == toCurrency)
                        {
                            newLoanValue.RateToUserCurrency = 1.0;
                            newLoanValue.ValueUserCurrency = newLoanValue.Value;
                        }
                        else
                        {
                            string apiRequest = newLoanValue.Date.ToString("yyyy-MM-dd");
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

                            newLoanValue.RateToUserCurrency = (double)JObject.Parse(task.Result).SelectToken("rates." + toCurrency);
                            newLoanValue.ValueUserCurrency = newLoanValue.Value * (decimal)newLoanValue.RateToUserCurrency;
                        }
                    }

                    return loanValueRepository.Update(newLoanValue);
                });

            Field<StringGraphType>(
                "deleteLoanValueById",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "loanValueId" }),
                resolve: context =>
                {
                    int loanValueId = context.GetArgument<int>("loanValueId");
                    try
                    {
                        loanValueRepository.DeleteById(loanValueId);
                        return $"The loan value with the id: {loanValueId} has been successfully deleted from the database.";
                    }
                    catch (System.Exception ex)
                    {
                        //return ex.ToString();     // for testing purposes
                        return null;
                    }
                });

            Field<StringGraphType>(
                "deleteLoanValuesByIds",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<IntGraphType>>> { Name = "loanValueIds" }),
                resolve: context =>
                {
                    int[] loanValueIds = context.GetArgument<int[]>("loanValueIds");
                    try
                    {
                        loanValueRepository.DeleteByIds(loanValueIds);

                        string loanValueIdsStr = string.Join(",", loanValueIds.Select(x => x.ToString()).ToArray());

                        return $"The loan values with the ids: [{loanValueIdsStr}] have been successfully deleted from the database.";
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
