using backendAPI.Types;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;

namespace backendAPI.Queries
{
    public class AccountValueQuery : ObjectGraphType
    {
        public AccountValueQuery(IAccountValueRepository accountValueRepository)
        {
            Field<ListGraphType<AccountValueType>>(
                "accountValues",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "accountId" }),
                resolve: context => accountValueRepository.GetAllForAccount(context.GetArgument<int>("accountId")));

            Field<AccountValueType>(
                "accountValue",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "accountValueId" }),
                resolve: context => accountValueRepository.GetById(context.GetArgument<int>("accountValueId")));
        }
    }
}
