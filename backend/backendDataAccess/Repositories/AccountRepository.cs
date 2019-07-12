using backendData;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace backendDataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly backendDbContext _dbContext;

        public AccountRepository(backendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account Add(Account account)
        {
            User user = _dbContext.Users
                            .Include(x => x.Country)
                            .Include(x => x.DisplayCurrency)
                            .SingleOrDefault(x => x.UserId == account.User.UserId);
            account.User = user;

            Currency currency = _dbContext.Currencies.SingleOrDefault(x => x.Code == account.QuotedCurrency.Code);
            account.QuotedCurrency = currency;

            addAccountAsync(account);
            _dbContext.SaveChanges();
            _dbContext.Entry<Account>(account).State = EntityState.Detached;
            _dbContext.Entry<Currency>(account.QuotedCurrency).State = EntityState.Detached;
            _dbContext.Entry<User>(account.User).State = EntityState.Detached;
            _dbContext.Entry<Currency>(account.User.DisplayCurrency).State = EntityState.Detached;
            _dbContext.SaveChanges();
            return account;
        }

        async public void addAccountAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
        }

        public void Delete(int accountId)
        {
            var account = _dbContext.Accounts
                                .Include(x => x.AccountValues)
                                .SingleOrDefault(x => x.AccountId == accountId);
            
            _dbContext.Remove(account);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Account> GetAllForUser(int userId)
        {
            return _dbContext.Accounts.AsNoTracking()
                .Where(x => x.User.UserId == userId)
                .Include(x => x.QuotedCurrency)
                .Include(x => x.User)
                    .ThenInclude(u => u.Country)
                .Include(x => x.User)
                    .ThenInclude(u => u.DisplayCurrency)
                .Include(x => x.AccountValues);
        }

        public Account GetById(int accountId)
        {
            return _dbContext.Accounts.AsNoTracking()
                .Include(x => x.QuotedCurrency)
                .Include(x => x.User)
                    .ThenInclude(u => u.Country)
                .Include(x => x.User)
                    .ThenInclude(u => u.DisplayCurrency)
                .Include(x => x.AccountValues)
                .SingleOrDefault(x => x.AccountId == accountId);
        }

        public Account Update(Account accountUpdates)
        {
            //User user = _dbContext.Users.SingleOrDefault(x => x.UserId == accountUpdates.User.UserId);
            //accountUpdates.User = user;

            Currency currency = _dbContext.Currencies.FirstOrDefault(x => x.Code == accountUpdates.QuotedCurrency.Code);
            accountUpdates.QuotedCurrency = currency;
            
            var account = _dbContext.Accounts
                .Include(x => x.QuotedCurrency)
                .Include(x => x.User)
                    .ThenInclude(u => u.Country)
                .Include(x => x.User)
                    .ThenInclude(u => u.DisplayCurrency)
                .Include(x => x.AccountValues)
                .SingleOrDefault(x => x.AccountId == accountUpdates.AccountId);

            if (account != null)
            {
                account.AccountName = accountUpdates.AccountName;
                account.Description = accountUpdates.Description;
                account.Type = accountUpdates.Type;
                account.Institution = accountUpdates.Institution;
                account.QuotedCurrency = accountUpdates.QuotedCurrency;
                account.IsActive = accountUpdates.IsActive;

                _dbContext.SaveChanges();

                //Note: need to detach to avoid tracking error when trying to update the same entry again with the same context
                //See: https://entityframeworkcore.com/knowledge-base/50987635/the-instance-of-entity-type--item--cannot-be-tracked-because-another-instance-with-the-same-key-value-for---id---is-already-being-tracked
                _dbContext.Entry<Account>(account).State = EntityState.Detached;
                _dbContext.Entry<Currency>(account.QuotedCurrency).State = EntityState.Detached;
                _dbContext.Entry<User>(account.User).State = EntityState.Detached;
                _dbContext.Entry<Currency>(account.User.DisplayCurrency).State = EntityState.Detached;
                _dbContext.SaveChanges();
            }
            return account;
        }
    }
}
