using backendAPI.Types;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;

namespace backendAPI.Queries
{
    public class LoanQuery : ObjectGraphType
    {
        public LoanQuery(ILoanRepository loanRepository)
        {
            Field<ListGraphType<LoanType>>(
                "userLoans",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "userId" }),
                resolve: context => loanRepository.GetAllForUser(context.GetArgument<int>("userId")));

            Field<LoanType>(
                "loan",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "loanId" }),
                resolve: context => loanRepository.GetById(context.GetArgument<int>("loanId")));
        }
    }
}
