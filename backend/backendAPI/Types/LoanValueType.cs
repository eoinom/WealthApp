using backendData.Models;
using GraphQL.Types;

namespace backendAPI.Types
{
    public class LoanValueType : ObjectGraphType<LoanValue>
    {
        public LoanValueType()
        {
            Field(x => x.LoanValueId, nullable: false);
            Field(x => x.Date, nullable: false).Description("The date the loan value relates to");
            Field(x => x.Value, nullable: true).Description("The value in the loan's quoted currency");
            Field(x => x.Loan, nullable: false, type: typeof(LoanType)).Description("The loan account which the account value applies to.");
        }
    }
}