using Alba;

namespace backendAPI.Tests
{
    public static class ScenarioExtensions
    {
        public static GraphQLExpectations GraphQL(this Scenario scenario)
        {
            return new GraphQLExpectations(scenario);
        }
    }
}
