using GraphQL.Common.Request;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace backendAPI.Tests
{
    public class LoanTests : SystemTestBase<Startup>
    {
        [Fact]
        public async Task userLoans_ReturnsValuesForSelectedFields()
        {
            var response = await run(_ =>
            {
                var input = new GraphQLRequest
                {
                    Query = @"
	                {
		                loan_queries {
			                userLoans(userId: 15) {
                                loanId
                                loanName
                                description      
                                type
                                startPrincipal
                                startDate
                                totalTerm
                                fixedTerm
                                rateType
                                aprRate
                                repaymentFrequency
                                repaymentAmount
                                isActive
                                institution
                                quotedCurrency {
                                    code
                                    nameShort
                                    nameLong        
                                }
                                loanValues {
                                    loanValueId
                                    date
                                    value
                                    rateToUserCurrency
                                    valueUserCurrency
                                    loan {
                                        loanId
                                    }
                                }
                            }
		                }
	                }"
                };
                _.Post.Json(input).ToUrl("/graphql");
                _.StatusCodeShouldBe(HttpStatusCode.OK);
            });

            string bodyString = response.ResponseBody.ReadAsText();
            JArray loans = (JArray)JObject.Parse(bodyString).SelectToken("data.loan_queries.userLoans");

            foreach (var loan in loans)
            {
                Assert.NotNull(loan.SelectToken("loanId"));
                Assert.NotNull(loan.SelectToken("loanName"));
                Assert.NotNull(loan.SelectToken("description"));
                Assert.NotNull(loan.SelectToken("type"));
                Assert.NotNull(loan.SelectToken("startPrincipal"));
                Assert.NotNull(loan.SelectToken("startDate"));
                Assert.NotNull(loan.SelectToken("totalTerm"));
                Assert.NotNull(loan.SelectToken("fixedTerm"));
                Assert.NotNull(loan.SelectToken("rateType"));
                Assert.NotNull(loan.SelectToken("aprRate"));
                Assert.NotNull(loan.SelectToken("repaymentFrequency"));
                Assert.NotNull(loan.SelectToken("repaymentAmount"));
                Assert.NotNull(loan.SelectToken("isActive"));
                Assert.NotNull(loan.SelectToken("institution"));
                Assert.NotNull(loan.SelectToken("quotedCurrency.code"));
                Assert.NotNull(loan.SelectToken("quotedCurrency.nameShort"));
                Assert.NotNull(loan.SelectToken("quotedCurrency.nameLong"));
                Assert.NotNull(loan.SelectToken("loanValues"));
            }
        }

        [Fact]
        public async Task loan_ReturnsCorrectLoan()
        {
            var response = await run(_ =>
            {
                var input = new GraphQLRequest
                {
                    Query = @"
	                {
		                loan_queries {
			                loan(loanId: 7) {
                                loanId
                                loanName
                                description      
                                type
                                startPrincipal
                                startDate
                                totalTerm
                                fixedTerm
                                rateType
                                aprRate
                                repaymentFrequency
                                repaymentAmount
                                isActive
                                institution
                                quotedCurrency {
                                    code
                                    nameShort
                                    nameLong        
                                }
                            }
		                }
	                }"
                };
                _.Post.Json(input).ToUrl("/graphql");
                _.StatusCodeShouldBe(HttpStatusCode.OK);
            });

            string bodyString = response.ResponseBody.ReadAsText();
            JObject loan = (JObject)JObject.Parse(bodyString).SelectToken("data.loan_queries.loan");

            Assert.Equal(7, loan.SelectToken("loanId"));
            Assert.Equal("Car Loan", loan.SelectToken("loanName"));
            Assert.Equal("Loan for new 2018 Ford Focus", loan.SelectToken("description"));
            Assert.Equal("Car Loan", loan.SelectToken("type"));
            Assert.Equal(18000, loan.SelectToken("startPrincipal"));
            Assert.Equal("2018-09-01", loan.SelectToken("startDate"));
            Assert.Equal(60, loan.SelectToken("totalTerm"));
            Assert.Equal(60, loan.SelectToken("fixedTerm"));
            Assert.Equal("Fixed", loan.SelectToken("rateType"));
            Assert.Equal(0.099, loan.SelectToken("aprRate"));
            Assert.Equal("Monthly", loan.SelectToken("repaymentFrequency"));
            Assert.Equal(381.56, loan.SelectToken("repaymentAmount"));
            Assert.Equal("True", loan.SelectToken("isActive"));
            Assert.Equal("Credit Union", loan.SelectToken("institution"));
            Assert.Equal("EUR", loan.SelectToken("quotedCurrency.code"));
            Assert.Equal("Euro", loan.SelectToken("quotedCurrency.nameShort"));
            Assert.Equal("Euro", loan.SelectToken("quotedCurrency.nameLong"));
        }
    }
}
