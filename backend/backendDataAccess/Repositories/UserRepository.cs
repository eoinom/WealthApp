using backendData;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace backendDataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly backendDbContext _dbContext;

        public UserRepository(backendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public User GetById(int id)
        {
            return _dbContext.Users.SingleOrDefault(x => x.UserId == id);
        }

        public User Add(User user)
        {            
            Country country = _dbContext.Countries.FirstOrDefault(c => c.Iso2Code == user.Country.Iso2Code);
            Currency currency = _dbContext.Currencies.FirstOrDefault(c => c.Code == user.DisplayCurrency.Code);
            user.Country = country;
            user.DisplayCurrency = currency;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

    }
}
