using backendData.Models;
using GraphQL.Types;

namespace backendAPI.Types
{
    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType()
        {
            Name = "Country";
            Description = "A country in the world.";
            Field(d => d.Iso2Code, nullable: false).Description("The ISO 3166-1 Alpha-2 code of the country.");
            Field(d => d.Iso3Code, nullable: true).Description("The ISO 3166-1 Alpha-3 code of the country.");
            Field(d => d.Name, nullable: false).Description("The name of the country.");
            Field(d => d.Continent, nullable: true).Description("The Continent the country is part of.");
        }
    }
}
