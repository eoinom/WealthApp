﻿using GraphQL.Types;

namespace backendAPI.Types
{
    public class LoanValueInputType : InputObjectGraphType
    {
        public LoanValueInputType()
        {
            Name = "LoanValueInputType";
            Field<IntGraphType>("loanValueId");
            Field<NonNullGraphType<DateGraphType>>("date");
            Field<NonNullGraphType<DecimalGraphType>>("value");
            Field<FloatGraphType>("rateToUserCurrency");
            Field<DecimalGraphType>("valueUserCurrency");
            Field<IntGraphType>("loanId");
        }
    }
}