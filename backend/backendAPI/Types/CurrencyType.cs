using backendData.Models;
using GraphQL.Types;

namespace backendAPI.Types
{
    public class CurrencyType : ObjectGraphType<Currency>
    {
        public CurrencyType()
        {
            Name = "Currency";
            Description = "A fiat currency to ISO 4217.";
            Field(x => x.Code, nullable: false).Description("The ISO 4217 three letter code for the currency.");
            Field(x => x.NameShort, nullable: true).Description("A shortened version of the currency name, e.g. Dollar.");
            Field(x => x.NameLong, nullable: true).Description("The ISO 4217 name of the currency.");
        }
    }
}
