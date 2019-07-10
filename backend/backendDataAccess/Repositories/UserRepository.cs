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

        public User Add(User user)
        {
            Country country = _dbContext.Countries.FirstOrDefault(x => x.Iso2Code == user.Country.Iso2Code);
            user.Country = country;

            Currency currency = _dbContext.Currencies.FirstOrDefault(x => x.Code == user.DisplayCurrency.Code);
            user.DisplayCurrency = currency;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            _dbContext.Entry<User>(user).State = EntityState.Detached;
            return user;
        }

        public bool CheckCredentials(string email, string password)
        {
            if (_dbContext.Users.AsNoTracking().SingleOrDefault(x => x.Email == email && x.Password == password) != null)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.AsNoTracking();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.AsNoTracking()
                .Include(x => x.Country)
                .Include(x => x.DisplayCurrency)
                .Include(x => x.Accounts)
                    .ThenInclude(a => a.QuotedCurrency)
                .Include(x => x.Accounts)
                    .ThenInclude(a => a.AccountValues)
                //.Include(x => x.CreditCards)
                //    .ThenInclude(c => c.QuotedCurrency)
                //.Include(x => x.CreditCards)
                //    .ThenInclude(c => c.CreditCardValues)
                .Include(x => x.Loans)
                    .ThenInclude(l => l.QuotedCurrency)
                .Include(x => x.Loans)
                    .ThenInclude(l => l.LoanValues)
                .AsNoTracking()
                .SingleOrDefault(x => x.UserId == id);
        }        

        public User Login(string email, string password)
        {
            return _dbContext.Users.AsNoTracking()
                .Include(x => x.Country).AsNoTracking()
                .Include(x => x.DisplayCurrency).AsNoTracking()
                .Include(x => x.Accounts)
                    .ThenInclude(a => a.QuotedCurrency).AsNoTracking()
                .Include(x => x.Accounts)
                    .ThenInclude(a => a.AccountValues).AsNoTracking()
                //.Include(x => x.CreditCards)
                //    .ThenInclude(c => c.QuotedCurrency)
                //.Include(x => x.CreditCards)
                //    .ThenInclude(c => c.CreditCardValues)
                .Include(x => x.Loans)
                    .ThenInclude(l => l.QuotedCurrency).AsNoTracking()
                .Include(x => x.Loans)
                    .ThenInclude(l => l.LoanValues).AsNoTracking()
                .SingleOrDefault(x => x.Email == email && x.Password == password);
        }

    }
}
