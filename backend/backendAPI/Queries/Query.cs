using backendAPI.Types;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;

namespace backendAPI.Queries
{
    public class Query : ObjectGraphType
    {
        public Query()
        {
            Name = "Query";
            Field<BankAccountQuery>("bankAccount_queries", resolve: context => new {} );
            Field<CountryQuery>("country_queries", resolve: context => new {} );
            Field<UserQuery>("user_queries", resolve: context => new {} );
        }
    }
}
