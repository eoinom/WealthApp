using GraphQL.Types;

namespace backendAPI.Types
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "UserInputType";
            Field<NonNullGraphType<StringGraphType>>("email");
            Field<NonNullGraphType<StringGraphType>>("password");
            Field<NonNullGraphType<StringGraphType>>("firstName");
            Field<NonNullGraphType<StringGraphType>>("lastName");
            Field<NonNullGraphType<StringGraphType>>("countryIso2Code");
            Field<BooleanGraphType>("newsletterSub");
            Field<StringGraphType>("displayCurrency");
        }
    }
}