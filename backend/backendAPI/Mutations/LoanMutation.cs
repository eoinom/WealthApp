using backendAPI.Types;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;
using Newtonsoft.Json.Linq;

namespace backendAPI.Mutations
{
    public class LoanMutation : ObjectGraphType
    {
        public LoanMutation(ILoanRepository loanRepository)
        {
            Name = "LoanMutations";

            Field<LoanType>(
                "addLoan",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoanInputType>> { Name = "loan" }),
                resolve: context =>
                {
                    //var loan = context.GetArgument<Loan>("loan");
                    var loanArg = context.Arguments["loan"];
                    
                    Currency loanCurrency = new Currency();
                    var currencyCode = loanArg != null
                        ? (string)JToken.FromObject(loanArg).SelectToken("quotedCurrency")
                        : null;
                    loanCurrency.Code = currencyCode;

                    User loanUser = new User();
                    int userId = loanArg != null
                        ? (int)JToken.FromObject(loanArg).SelectToken("userId")
                        : 0;
                    loanUser.UserId = userId;

                    Loan newLoan = new Loan()
                    {                        
                        LoanName = (string)JToken.FromObject(loanArg).SelectToken("loanName"),
                        Description = (string)JToken.FromObject(loanArg).SelectToken("description"),
                        Type = (string)JToken.FromObject(loanArg).SelectToken("type"),

                        StartPrincipal = (decimal)JToken.FromObject(loanArg).SelectToken("startPrincipal"),
                        StartDate = (System.DateTime)JToken.FromObject(loanArg).SelectToken("startDate"),
                        TotalTerm = (int)JToken.FromObject(loanArg).SelectToken("totalTerm"),
                        FixedTerm = (int)JToken.FromObject(loanArg).SelectToken("fixedTerm"),
                        RateType = (string)JToken.FromObject(loanArg).SelectToken("rateType"),
                        AprRate = (double)JToken.FromObject(loanArg).SelectToken("aprRate"),
                        RepaymentFrequency = (string)JToken.FromObject(loanArg).SelectToken("repaymentFrequency"),
                        RepaymentAmount = (decimal)JToken.FromObject(loanArg).SelectToken("repaymentAmount"),                        

                        IsActive = (bool)JToken.FromObject(loanArg).SelectToken("isActive"),
                        Institution = (string)JToken.FromObject(loanArg).SelectToken("institution"),
                        QuotedCurrency = loanCurrency,
                        User = loanUser
                    };

                    return loanRepository.Add(newLoan);
                });

            Field<LoanType>(
                "updateLoan",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoanInputType>> { Name = "loan" }),
                resolve: context =>
                {
                    var loanArg = context.Arguments["loan"];

                    Currency loanCurrency = new Currency();
                    var currencyCode = loanArg != null
                        ? (string)JToken.FromObject(loanArg).SelectToken("quotedCurrency")
                        : null;
                    loanCurrency.Code = currencyCode;

                    User loanUser = new User();
                    int userId = loanArg != null
                        ? (int)JToken.FromObject(loanArg).SelectToken("userId")
                        : 0;
                    loanUser.UserId = userId;

                    Loan newLoan = new Loan()
                    {
                        LoanId = (int)JToken.FromObject(loanArg).SelectToken("loanId"),
                        LoanName = (string)JToken.FromObject(loanArg).SelectToken("loanName"),
                        Description = (string)JToken.FromObject(loanArg).SelectToken("description"),
                        Type = (string)JToken.FromObject(loanArg).SelectToken("type"),

                        StartPrincipal = (decimal)JToken.FromObject(loanArg).SelectToken("startPrincipal"),
                        StartDate = (System.DateTime)JToken.FromObject(loanArg).SelectToken("startDate"),
                        TotalTerm = (int)JToken.FromObject(loanArg).SelectToken("termInMonths"),
                        FixedTerm = (int)JToken.FromObject(loanArg).SelectToken("fixedTermInMonths"),
                        RateType = (string)JToken.FromObject(loanArg).SelectToken("rateType"),
                        AprRate = (double)JToken.FromObject(loanArg).SelectToken("aprRate"),
                        RepaymentFrequency = (string)JToken.FromObject(loanArg).SelectToken("repaymentFrequency"),
                        RepaymentAmount = (decimal)JToken.FromObject(loanArg).SelectToken("repaymentAmount"),

                        IsActive = (bool)JToken.FromObject(loanArg).SelectToken("isActive"),
                        Institution = (string)JToken.FromObject(loanArg).SelectToken("institution"),
                        QuotedCurrency = loanCurrency,
                        User = loanUser
                    };

                    return loanRepository.Update(newLoan);
                });

            Field<StringGraphType>(
                "deleteLoan",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "loanId" }),
                resolve: context =>
                {
                    int loanId = context.GetArgument<int>("loanId");
                    try
                    {
                        loanRepository.Delete(loanId);
                        return $"The loan with the id: {loanId} has been successfully deleted from the database.";
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
