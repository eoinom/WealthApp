using GraphQL.Common.Request;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace backendAPI.Tests
{
    public class AccountTests : SystemTestBase<Startup>
    {
        [Fact]
        public async Task userAccounts_ReturnsValuesForSelectedFields()
        {
            var response = await run(_ =>
            {
                var input = new GraphQLRequest
                {
                    Query = @"
	                {
		                account_queries {
			                userAccounts(userId: 16) {
                                accountId
                                accountName
                                description
                                type
                                isActive
                                institution
                                quotedCurrency {
                                    code
                                }
                                user {
                                    userId
                                }
                            }
		                }
	                }"
                };
                _.Post.Json(input).ToUrl("/graphql");
                _.StatusCodeShouldBe(HttpStatusCode.OK);
            });

            string bodyString = response.ResponseBody.ReadAsText();
            JArray accounts = (JArray)JObject.Parse(bodyString).SelectToken("data.account_queries.userAccounts");

            foreach (var account in accounts)
            {
                Assert.NotNull(account.SelectToken("accountId"));
                Assert.NotNull(account.SelectToken("accountName"));
                Assert.NotNull(account.SelectToken("description"));
                Assert.NotNull(account.SelectToken("type"));
                Assert.NotNull(account.SelectToken("isActive"));
                Assert.NotNull(account.SelectToken("institution"));
                Assert.NotNull(account.SelectToken("quotedCurrency"));
                Assert.NotNull(account.SelectToken("user"));
            }
        }

        [Fact]
        public async Task account_ReturnsCorrectAccount()
        {
            var response = await run(_ =>
            {
                var input = new GraphQLRequest
                {
                    Query = @"
	                {
		                account_queries {
			                account(accountId: 7) {
                                accountId
                                accountName
                                description
                                type
                                isActive
                                institution
                                quotedCurrency {
                                    code
                                }
                                user {
                                    userId
                                }
                            }
		                }
	                }"
                };
                _.Post.Json(input).ToUrl("/graphql");
                _.StatusCodeShouldBe(HttpStatusCode.OK);
                //_.GraphQL().ShouldBeSuccess(@"
                //    { 
                //        ""account_queries"": { 
                //            ""account"": { 
                //                ""accountId"": 7 
                //                ""accountName"": ""Savings A/C"" 
                //                ""description"": ""Savings account for new car"" 
                //                ""type"": ""Savings"" 
                //                ""isActive"": ""True"" 
                //                ""institution"": ""Permanent TSB"" 
                //                ""quotedCurrency"": {
                //                    ""code"": ""EUR"" 
                //                }
                //                ""user"": {
                //                    ""userId"": 16
                //                }
                //            }
                //        }
                //    }");
                //_.GraphQL().ShouldBeSuccess(@"
                //    { 
                //        account_queries: { 
                //            account: { 
                //                accountId: 7
                //                accountName: 'Savings A/C'
                //                description: 'Savings account for new car'
                //                type: 'Savings'
                //                isActive: True
                //                institution: 'Permanent TSB' 
                //                quotedCurrency: {
                //                    code: 'EUR'
                //                }
                //                user: {
                //                    userId: 16
                //                }
                //            }
                //        }
                //    }");
            });

            string bodyString = response.ResponseBody.ReadAsText();
            JObject account = (JObject)JObject.Parse(bodyString).SelectToken("data.account_queries.account");

            Assert.Equal(7, account.SelectToken("accountId"));
            Assert.Equal("Savings A/C", account.SelectToken("accountName"));
            Assert.Equal("Savings account for new car", account.SelectToken("description"));
            Assert.Equal("Savings", account.SelectToken("type"));
            Assert.Equal("True", account.SelectToken("isActive"));
            Assert.Equal("Permanent TSB", account.SelectToken("institution"));
            Assert.Equal("EUR", account.SelectToken("quotedCurrency.code"));
            Assert.Equal("16", account.SelectToken("user.userId"));
        }
    }
}
