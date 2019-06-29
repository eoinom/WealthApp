using backendData;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace backendDataAccess.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly backendDbContext _dbContext;

        public BankAccountRepository(backendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BankAccount Add(BankAccount bankAccount)
        {
            User user = _dbContext.Users.SingleOrDefault(x => x.UserId == bankAccount.User.UserId);
            bankAccount.User = user;

            Currency currency = _dbContext.Currencies.SingleOrDefault(x => x.Code == bankAccount.QuotedCurrency.Code);
            bankAccount.QuotedCurrency = currency;

            addBankAccountAsync(bankAccount);
            _dbContext.SaveChanges();
            return bankAccount;
        }

        async public void addBankAccountAsync(BankAccount bankAccount)
        {
            await _dbContext.BankAccounts.AddAsync(bankAccount);
        }

        public void Delete(int bankAccountId)
        {
            //var bankAccount = new BankAccount() { BankAccountId = bankAccountId };
            var bankAccount = _dbContext.BankAccounts
                                .Include(x => x.AccountValues)
                                .SingleOrDefault(x => x.BankAccountId == bankAccountId);

            //_dbContext.Remove<BankAccount>(bankAccount);

            _dbContext.Remove(bankAccount);
            _dbContext.SaveChanges();
        }

        public IEnumerable<BankAccount> GetAllForUser(int userId)
        {
            return _dbContext.BankAccounts
                .Where(x => x.User.UserId == userId)
                .Include(x => x.QuotedCurrency)
                .Include(x => x.User)
                .Include(x => x.AccountValues);
        }

        public BankAccount GetById(int id)
        {
            return _dbContext.BankAccounts
                .Include(x => x.QuotedCurrency)
                .Include(x => x.User)
                .Include(x => x.AccountValues)
                .SingleOrDefault(x => x.BankAccountId == id);
        }

        public BankAccount Update(BankAccount accountUpdates)
        {
            User user = _dbContext.Users.SingleOrDefault(x => x.UserId == accountUpdates.User.UserId);
            accountUpdates.User = user;

            Currency currency = _dbContext.Currencies.FirstOrDefault(x => x.Code == accountUpdates.QuotedCurrency.Code);
            accountUpdates.QuotedCurrency = currency;
            
            var account = _dbContext.BankAccounts.SingleOrDefault(x => x.BankAccountId == accountUpdates.BankAccountId);

            if (account != null)
            {
                account.AccountName = accountUpdates.AccountName;
                account.Description = accountUpdates.Description;
                account.Type = accountUpdates.Type;
                account.Institution = accountUpdates.Institution;
                account.QuotedCurrency = accountUpdates.QuotedCurrency;
                account.IsActive = accountUpdates.IsActive;
                _dbContext.SaveChanges();
            }

            return account;
        }
    }
}
