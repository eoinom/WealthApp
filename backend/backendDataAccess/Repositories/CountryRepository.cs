using backendData;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace backendDataAccess.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly backendDbContext _dbContext;

        public CountryRepository(backendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Country> GetAll()
        {
            return _dbContext.Countries;
        }

        public Country GetByIso2Code(string code)
        {
            return _dbContext.Countries.SingleOrDefault(x => x.Iso2Code == code);
        }

        public Country GetByIso3Code(string code)
        {
            return _dbContext.Countries.SingleOrDefault(x => x.Iso3Code == code);
        }
    }
}
