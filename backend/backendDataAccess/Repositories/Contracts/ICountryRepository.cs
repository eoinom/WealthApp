using backendData.Models;
using System.Collections.Generic;

namespace backendDataAccess.Repositories.Contracts
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAll();
        Country GetByIso2Code(string code);
        Country GetByIso3Code(string code);
    }
}
