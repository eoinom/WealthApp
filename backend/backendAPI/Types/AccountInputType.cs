using GraphQL.Types;

namespace backendAPI.Types
{
    public class AccountInputType : InputObjectGraphType
    {
        public AccountInputType()
        {
            Name = "AccountInputType";
            Field<IntGraphType>("accountId");
            Field<NonNullGraphType<StringGraphType>>("accountName");
            Field<StringGraphType>("description");
            Field<StringGraphType>("type");
            Field<BooleanGraphType>("isActive");
            Field<StringGraphType>("institution");
            Field<NonNullGraphType<StringGraphType>>("quotedCurrency");
            Field<NonNullGraphType<IntGraphType>>("userId");
        }
    }
}