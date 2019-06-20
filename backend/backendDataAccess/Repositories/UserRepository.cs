using backendData;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
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
            return _dbContext.Users
                .Include(x => x.Country)
                .Include(x => x.DisplayCurrency)
                .Include(x => x.BankAccounts)
                    .ThenInclude(acc => acc.QuotedCurrency)
                .Include(x => x.BankAccounts)
                    .ThenInclude(acc => acc.AccountValues)
                .SingleOrDefault(x => x.UserId == id);
        }

        public User Add(User user)
        {            
            Country country = _dbContext.Countries.FirstOrDefault(x => x.Iso2Code == user.Country.Iso2Code);
            user.Country = country;

            Currency currency = _dbContext.Currencies.FirstOrDefault(x => x.Code == user.DisplayCurrency.Code);
            user.DisplayCurrency = currency;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public User CheckCredentials(string email, string password)
        {
            return _dbContext.Users                
                .Include(x => x.Country)
                .Include(x => x.DisplayCurrency)
                .Include(x => x.BankAccounts)
                    .ThenInclude(acc => acc.QuotedCurrency)
                .Include(x => x.BankAccounts)
                    .ThenInclude(acc => acc.AccountValues)
                .SingleOrDefault(x => x.Email == email && x.Password == password);
        }

    }
}
