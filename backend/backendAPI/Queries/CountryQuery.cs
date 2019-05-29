using backendAPI.Types;
using backendDataAccess.Repositories.Contracts;
using GraphQL.Types;

namespace backendAPI.Queries
{
    public class CountryQuery : ObjectGraphType
    {
        public CountryQuery(ICountryRepository countryRepository)
        {
            Field<ListGraphType<CountryType>>(
                "countries",
                resolve: context => countryRepository.GetAll());

            Field<CountryType>(
                "country",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "iso2code" }),
                resolve: context => countryRepository.GetByIso2Code(context.GetArgument<string>("iso2code")));

            Field<CountryType>(
                "country",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "iso3code" }),
                resolve: context => countryRepository.GetByIso3Code(context.GetArgument<string>("iso3code")));
        }
    }
}
