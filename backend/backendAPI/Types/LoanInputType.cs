using GraphQL.Types;

namespace backendAPI.Types
{
    public class LoanInputType : InputObjectGraphType
    {
        public LoanInputType()
        {
            Name = "LoanInputType";
            Field<IntGraphType>("loanId");
            Field<NonNullGraphType<StringGraphType>>("loanName");
            Field<StringGraphType>("description");
            Field<StringGraphType>("type");

            Field<DecimalGraphType>("startPrincipal");
            Field<DateGraphType>("startDate");
            Field<IntGraphType>("totalTerm");
            Field<IntGraphType>("fixedTerm");
            Field<StringGraphType>("rateType");
            Field<FloatGraphType>("aprRate");
            Field<StringGraphType>("repaymentFrequency");
            Field<DecimalGraphType>("repaymentAmount");
            
            Field<BooleanGraphType>("isActive");
            Field<StringGraphType>("institution");
            Field<NonNullGraphType<StringGraphType>>("quotedCurrency");
            Field<NonNullGraphType<IntGraphType>>("userId");
        }
    }
}