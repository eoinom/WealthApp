using backendAPI.Types;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;

namespace backendAPI.Queries
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(IUserRepository userRepository)
        {
            Field<ListGraphType<UserType>>(
                "users",
                resolve: context => userRepository.GetAll());

            Field<UserType>(
                "user",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => userRepository.GetById(context.GetArgument<int>("id")));

            Field<UserType>(
                "userLogin",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "email" }, new QueryArgument<StringGraphType> { Name = "password" }),
                resolve: context => userRepository.Login(context.GetArgument<string>("email"), context.GetArgument<string>("password")));
        }
    }
}
