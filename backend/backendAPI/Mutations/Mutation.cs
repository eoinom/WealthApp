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
            Name = "Mutation";
            Field<BankAccountMutation>("bankAccount_mutations", resolve: context => new { } );
            Field<UserMutation>("user_mutations", resolve: context => new { } );
        }
    }
}
