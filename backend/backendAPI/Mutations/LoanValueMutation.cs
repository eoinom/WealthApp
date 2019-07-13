using backendAPI.Types;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace backendAPI.Mutations
{
    public class LoanValueMutation : ObjectGraphType
    {
        public LoanValueMutation(ILoanValueRepository loanValueRepository)
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
                        Value = (decimal)JToken.FromObject(loanValueArg).SelectToken("value"),
                        RateToUserCurrency = (double)JToken.FromObject(loanValueArg).SelectToken("rateToUserCurrency"),
                        ValueUserCurrency = (decimal)JToken.FromObject(loanValueArg).SelectToken("valueUserCurrency")
                    };
                   
                    var loanId = JToken.FromObject(loanValueArg).SelectToken("loanId");
                    if (loanId != null)
                    {
                        Loan loan = new Loan();
                        loan.LoanId = (int)loanId;
                        newLoanValue.Loan = loan;
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
                        Value = (decimal)JToken.FromObject(loanValueArg).SelectToken("value"),
                        RateToUserCurrency = (double)JToken.FromObject(loanValueArg).SelectToken("rateToUserCurrency"),
                        ValueUserCurrency = (decimal)JToken.FromObject(loanValueArg).SelectToken("valueUserCurrency")
                    };

                    Loan loan = new Loan();
                    var loanId = JToken.FromObject(loanValueArg).SelectToken("loanId");
                    if (loanId != null)
                    {
                        loan.LoanId = (int)loanId;
                        newLoanValue.Loan = loan;
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
