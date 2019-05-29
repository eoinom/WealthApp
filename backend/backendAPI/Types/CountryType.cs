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
            Field(x => x.Iso2Code, nullable: false).Description("The ISO 3166-1 Alpha-2 code of the country.");
            Field(x => x.Iso3Code, nullable: true).Description("The ISO 3166-1 Alpha-3 code of the country.");
            Field(x => x.Name, nullable: true).Description("The name of the country.");
            Field(x => x.Continent, nullable: true).Description("The Continent the country is part of.");
            Field(x => x.MainCurrency, nullable: true, type: typeof(CurrencyType)).Description("The main currency of the country.");
        }
    }
}
