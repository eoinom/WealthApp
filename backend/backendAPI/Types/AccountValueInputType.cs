using GraphQL.Types;

namespace backendAPI.Types
{
    public class AccountValueInputType : InputObjectGraphType
    {
        public AccountValueInputType()
        {
            Name = "AccountValueInputType";
            Field<NonNullGraphType<DateGraphType>>("date");
            Field<NonNullGraphType<DecimalGraphType>>("value");
            Field<IntGraphType>("bankAccountId");
            Field<IntGraphType>("cashAccountId");
            Field<IntGraphType>("loanAccountId");
            Field<IntGraphType>("mortgageAccountId");
        }
    }
}