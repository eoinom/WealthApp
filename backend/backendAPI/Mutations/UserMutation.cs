using backendAPI.Types;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;
using Newtonsoft.Json.Linq;

namespace backendAPI.Mutations
{
    public class UserMutation : ObjectGraphType
    {
        public UserMutation(IUserRepository userRepository)
        {
            Name = "AddUserMutation";

            Field<UserType>(
                "addUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }),
                resolve: context =>
                {
                    //var user = context.GetArgument<User>("user");
                    var userArg = context.Arguments["user"];
                    Country userCountry = new Country();
                    //userCountry.Iso2Code = JToken.FromObject(userArg).
                    //var userArg = context.Arguments["user"];

                    var countryCode = userArg != null
                        ? (string)JToken.FromObject(userArg).SelectToken("countryIso2Code")
                        : null;
                    userCountry.Iso2Code = countryCode;                    

                    //User newUser = new User()
                    //{
                    //    FirstName = (string)JToken.FromObject(userArg).SelectToken("firstName"),
                    //    LastName = (string)JToken.FromObject(userArg).SelectToken("lastName"),
                    //    Email = (string)JToken.FromObject(userArg).SelectToken("email"),
                    //    Password = (string)JToken.FromObject(userArg).SelectToken("password"),
                    //    Country = userCountry,
                    //    NewsletterSub = JToken.FromObject(userArg).SelectToken("newsletterSub") != null
                    //                    ? (bool)JToken.FromObject(userArg).SelectToken("newsletterSub")
                    //                    : false
                    //};

                    var newUser = userArg != null
                        ? JToken.FromObject(userArg).ToObject<User>()
                        : null;
                    newUser.Country = userCountry;

                    return userRepository.Add(newUser);
                });
        }
    }
}
