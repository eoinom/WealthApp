using backendAPI.Types;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;

namespace backendAPI.Queries
{
    public class BankAccountQuery : ObjectGraphType
    {
        public BankAccountQuery(IBankAccountRepository bankAccountRepository)
        {
            Field<ListGraphType<BankAccountType>>(
                "userBankAccounts",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "userId" }),
                resolve: context => bankAccountRepository.GetAllForUser(context.GetArgument<int>("userId")));

            Field<BankAccountType>(
                "bankAccount",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "accountId" }),
                resolve: context => bankAccountRepository.GetById(context.GetArgument<int>("accountId")));
        }
    }
}
