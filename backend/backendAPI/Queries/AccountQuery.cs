using backendAPI.Types;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;

namespace backendAPI.Queries
{
    public class AccountQuery : ObjectGraphType
    {
        public AccountQuery(IAccountRepository accountRepository)
        {
            Field<ListGraphType<LoanType>>(
                "userAccounts",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "userId" }),
                resolve: context => accountRepository.GetAllForUser(context.GetArgument<int>("userId")));

            Field<LoanType>(
                "account",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "accountId" }),
                resolve: context => accountRepository.GetById(context.GetArgument<int>("accountId")));
        }
    }
}
