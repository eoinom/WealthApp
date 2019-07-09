using backendAPI.Types;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;
using Newtonsoft.Json.Linq;

namespace backendAPI.Mutations
{
    public class Mutation : ObjectGraphType
    {
        public Mutation()
        {
            Name = "Mutations";            

            Field<AccountMutation>("account_mutations", resolve: context => new { } );
            Field<AccountValueMutation>("accountValue_mutations", resolve: context => new { });
            Field<LoanMutation>("loan_mutations", resolve: context => new { });
            Field<LoanValueMutation>("loanValue_mutations", resolve: context => new { });
            Field<UserMutation>("user_mutations", resolve: context => new { } );
        }
    }
}
