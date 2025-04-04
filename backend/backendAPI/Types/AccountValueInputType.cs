﻿using GraphQL.Types;

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
            Field<FloatGraphType>("rateToUserCurrency");
            Field<DecimalGraphType>("valueUserCurrency");
            Field<IntGraphType>("accountId");
        }
    }
}