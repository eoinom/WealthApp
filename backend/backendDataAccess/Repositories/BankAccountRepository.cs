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
            User user = _dbContext.Users.FirstOrDefault(x => x.UserId == bankAccount.User.UserId);
            bankAccount.User = user;

            Currency currency = _dbContext.Currencies.FirstOrDefault(x => x.Code == bankAccount.QuotedCurrency.Code);
            bankAccount.QuotedCurrency = currency;

            _dbContext.BankAccounts.Add(bankAccount);
            _dbContext.SaveChanges();
            return bankAccount;
        }

        public IEnumerable<BankAccount> GetAllForUser(int userId)
        {
            return _dbContext.BankAccounts
                .Where(x => x.User.UserId == userId)
                .Include(x => x.QuotedCurrency)
                .Include(x => x.User);
        }

        public BankAccount GetById(int id)
        {
            return _dbContext.BankAccounts.SingleOrDefault(x => x.BankAccountId == id);
        }        
    }
}
