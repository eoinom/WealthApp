using backendData.Models;
using backendDataAccess.Repositories;
using backendDataAccess.Tests.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace backendDataAccess.Tests
{
    public class CountryRepositoryTests : backendDataAccessTestBase
    {
        [Fact]
        public void GetAll_ReturnsCorrectType()
        {
            // Arrange
            var countryRepo = new CountryRepository(_context);

            // Act
            var result = countryRepo.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Country>>(result);
        }

        [Fact]
        public void GetAll_ReturnsAllCountries()
        {
            var countryRepo = new CountryRepository(_context);

            var result = countryRepo.GetAll();

            var countries = Assert.IsAssignableFrom<IEnumerable<Country>>(result);
            Assert.Equal(6, countries.Count());
        }

        [Fact]
        public void GetByIso2Code_ReturnsCountry_GivenValidCode()
        {
            var countryRepo = new CountryRepository(_context);

            var result = countryRepo.GetByIso2Code("IE");

            var country = Assert.IsAssignableFrom<Country>(result);
            Assert.Equal("Ireland", country.Name);
        }

        [Fact]
        public void GetByIso2Code_ReturnsNull_GivenInvalidId()
        {
            var countryRepo = new CountryRepository(_context);

            var result = countryRepo.GetByIso2Code("IX");

            Assert.Null(result);
        }

        [Fact]
        public void GetByIso3Code_ReturnsCountry_GivenValidCode()
        {
            var countryRepo = new CountryRepository(_context);

            var result = countryRepo.GetByIso3Code("AUS");

            var country = Assert.IsAssignableFrom<Country>(result);
            Assert.Equal("Australia", country.Name);
        }

        [Fact]
        public void GetByIso3Code_ReturnsNull_GivenInvalidId()
        {
            var countryRepo = new CountryRepository(_context);

            var result = countryRepo.GetByIso3Code("AUX");

            Assert.Null(result);
        }

    }
}
