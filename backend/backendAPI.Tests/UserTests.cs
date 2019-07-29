using GraphQL.Common.Request;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace backendAPI.Tests
{
    public class UserTests : SystemTestBase<Startup>
    {
        [Fact]
        public async Task users_ReturnsCorrectNumber()
        {
            var response = await run(_ =>
            {
                var input = new GraphQLRequest
                {
                    Query = @"
	                {
		                user_queries {
			                users {
                                userId
                                firstName
                                lastName
                                email
                            }
		                }
	                }"
                };
                _.Post.Json(input).ToUrl("/graphql");
                _.StatusCodeShouldBe(HttpStatusCode.OK);
                //_.GraphQL().ShouldBeSuccess(@"{ ""hero"": { ""name"": ""R2-D2"" }}");
            });

            //var contentJson = await SendRequest(request);
            string bodyString = response.ResponseBody.ReadAsText();
            JArray users = (JArray)JObject.Parse(bodyString).SelectToken("data.user_queries.users");

            Assert.Equal(14, users.Count);
        }

        [Fact]
        public async Task users_ReturnsValuesForSelectedFields()
        {
            var response = await run(_ =>
            {
                var input = new GraphQLRequest
                {
                    Query = @"
	                {
		                user_queries {
			                users {
                                userId
                                firstName
                                lastName
                                email
                            }
		                }
	                }"
                };
                _.Post.Json(input).ToUrl("/graphql");
                _.StatusCodeShouldBe(HttpStatusCode.OK);
            });

            //var contentJson = await SendRequest(request);
            string bodyString = response.ResponseBody.ReadAsText();
            JArray users = (JArray)JObject.Parse(bodyString).SelectToken("data.user_queries.users");

            foreach (var user in users)
            {
                Assert.NotNull(user.SelectToken("userId"));
                Assert.NotNull(user.SelectToken("firstName"));
                Assert.NotNull(user.SelectToken("lastName"));
                Assert.NotNull(user.SelectToken("email"));
            }
        }

        [Fact]
        public async Task user_ReturnsCorrectUser()
        {
            var response = await run(_ =>
            {
                var input = new GraphQLRequest
                {
                    Query = @"
	                {
		                user_queries {
			                user(id: 16) {
                                userId
                                firstName
                                lastName
                                email
                            }
		                }
	                }"
                };
                _.Post.Json(input).ToUrl("/graphql");
                _.StatusCodeShouldBe(HttpStatusCode.OK);
            });

            //var contentJson = await SendRequest(request);
            string bodyString = response.ResponseBody.ReadAsText();
            JObject user = (JObject)JObject.Parse(bodyString).SelectToken("data.user_queries.user");

            Assert.Equal(16, user.SelectToken("userId"));
            Assert.Equal("Joe", user.SelectToken("firstName"));
            Assert.Equal("Bloggs", user.SelectToken("lastName"));
            Assert.Equal("joe.bloggs@gmail.com", user.SelectToken("email"));
        }

        [Fact]
        public async Task userLogin_ReturnsCorrectUser()
        {
            var response = await run(_ =>
            {
                var input = new GraphQLRequest
                {
                    Query = @"
	                {
		                user_queries {
			                userLogin(email: ""joe.bloggs@gmail.com"", password: ""pass1234"") {
                                userId
                                firstName
                                lastName
                                email
                            }
		                }
	                }"
                };
                _.Post.Json(input).ToUrl("/graphql");
                _.StatusCodeShouldBe(HttpStatusCode.OK);
            });

            //var contentJson = await SendRequest(request);
            string bodyString = response.ResponseBody.ReadAsText();
            JObject user = (JObject)JObject.Parse(bodyString).SelectToken("data.user_queries.userLogin");

            Assert.Equal(16, user.SelectToken("userId"));
            Assert.Equal("Joe", user.SelectToken("firstName"));
            Assert.Equal("Bloggs", user.SelectToken("lastName"));
            Assert.Equal("joe.bloggs@gmail.com", user.SelectToken("email"));
        }
    }
}
