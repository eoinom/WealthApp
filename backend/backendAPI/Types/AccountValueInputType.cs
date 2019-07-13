using GraphQL.Types;

namespace backendAPI.Types
{
    public class AccountValueInputType : InputObjectGraphType
    {
        public AccountValueInputType()
        {
            Name = "AccountValueInputType";
            Field<IntGraphType>("accountValueId");
            Field<NonNullGraphType<DateGraphType>>("date");
            Field<NonNullGraphType<DecimalGraphType>>("value");
            Field<NonNullGraphType<FloatGraphType>>("rateToUserCurrency");
            Field<NonNullGraphType<DecimalGraphType>>("valueUserCurrency");
            Field<IntGraphType>("accountId");
        }
    }
}