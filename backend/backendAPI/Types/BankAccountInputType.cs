using GraphQL.Types;

namespace backendAPI.Types
{
    public class BankAccountInputType : InputObjectGraphType
    {
        public BankAccountInputType()
        {
            Name = "BankAccountInputType";
            Field<IntGraphType>("bankAccountId");
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