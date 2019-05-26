using backendData.Models;
using GraphQL.Types;

namespace backendAPI.Types
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "UserInput";
            Field<NonNullGraphType<StringGraphType>>("email");
            Field<NonNullGraphType<StringGraphType>>("password");
            Field<NonNullGraphType<StringGraphType>>("firstname");
            Field<NonNullGraphType<StringGraphType>>("lastname");
            //Field<CountryType>("country");
            Field<BooleanGraphType>("newsletterSub");
            //Field<StringGraphType>("displayCurrency");
        }
    }
}