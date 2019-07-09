using backendAPI.Types;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;

namespace backendAPI.Queries
{
    public class LoanValueQuery : ObjectGraphType
    {
        public LoanValueQuery(ILoanValueRepository loanValueRepository)
        {
            Field<ListGraphType<LoanValueType>>(
                "loanValues",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "loanId" }),
                resolve: context => loanValueRepository.GetAllForLoan(context.GetArgument<int>("loanId")));

            Field<AccountValueType>(
                "loanValue",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "loanValueId" }),
                resolve: context => loanValueRepository.GetById(context.GetArgument<int>("loanValueId")));
        }
    }
}
