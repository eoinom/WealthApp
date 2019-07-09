using backendAPI.Types;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;

namespace backendAPI.Queries
{
    public class Query : ObjectGraphType
    {
        public Query()
        {
            Name = "Queries";
            
            Field<AccountQuery>("account_queries", resolve: context => new {} );
            Field<AccountValueQuery>("accountValue_queries", resolve: context => new { });
            Field<CountryQuery>("country_queries", resolve: context => new {} );            
            Field<LoanQuery>("loan_queries", resolve: context => new { });
            Field<LoanValueQuery>("loanValue_queries", resolve: context => new { });
            Field<UserQuery>("user_queries", resolve: context => new {} );
        }
    }
}
