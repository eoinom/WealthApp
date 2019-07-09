using backendData.Models;
using GraphQL.Types;

namespace backendAPI.Types
{
    public class LoanType : ObjectGraphType<Loan>
    {
        public LoanType()
        {
            Field(x => x.LoanId, nullable: false);
            Field(x => x.LoanName, nullable: false).Description("The user's name for the loan, e.g. My Car Loan");
            Field(x => x.Description, nullable: true).Description("The user's description for the loan");
            Field(x => x.Type, nullable: true).Description("The type of loan, e.g. mortgage, personal loan, car loan, student loan etc.");

            Field(x => x.StartPrincipal, nullable: true).Description("The amount borrowed at the start of the loan");
            Field(x => x.StartDate, nullable: true).Description("The start date for the loan");
            Field(x => x.TotalTerm, nullable: true).Description("The total number of repayment periods in the specified payment frequency, e.g. 10 year term repaying monthly = 120");
            Field(x => x.FixedTerm, nullable: true).Description("The number of repayment periods during the fixed rate repayment period (if applicable) in the specified payment frequency, e.g. 2 years fixed term repaying weekly = 104");
            Field(x => x.RateType, nullable: true).Description("The type of rate applied to the loan, e.g. fixed or variable");
            Field(x => x.AprRate, nullable: true).Description("The loan interest rate in Annual Percentage Rate as a decimal, e.g. 4.9% = 0.049");
            Field(x => x.RepaymentFrequency, nullable: true).Description("The repayment frequency, e.g. Weekly, Bi-Weekly, Monthly, Bi-Monthly, Quarterly, Half-Annually, Annually");
            Field(x => x.RepaymentAmount, nullable: true).Description("The amount that is due each repayment period");

            Field(x => x.IsActive, nullable: true).Description("True if the loan is currently active, i.e. not closed.");
            Field(x => x.Institution, nullable: false).Description("The institution where the loan is held, e.g. bank name.");
            Field(x => x.QuotedCurrency, nullable: false, type: typeof(CurrencyType)).Description("The currency which the loan values are quoted in.");
            Field(x => x.User, nullable: false, type: typeof(UserType)).Description("The user which owns the loan.");
            Field(x => x.LoanValues, nullable: false, type: typeof(ListGraphType<LoanValueType>)).Description("The loan balance datapoints.");
        }
    }
}