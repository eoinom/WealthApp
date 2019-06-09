using backendData;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace backendDataAccess.Repositories
{
    public class AccountValueRepository : IAccountValueRepository
    {
        private readonly backendDbContext _dbContext;

        public AccountValueRepository(backendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AccountValue Add(AccountValue accountValue)
        {
            BankAccount bankAccount = _dbContext.BankAccounts.FirstOrDefault(x => x.BankAccountId == accountValue.BankAccount.BankAccountId);
            accountValue.BankAccount = bankAccount;

            _dbContext.AccountValues.Add(accountValue);
            _dbContext.SaveChanges();
            return accountValue;
        }

        public IEnumerable<AccountValue> GetAllForBankAccount(int bankAccountId)
        {
            return _dbContext.AccountValues
                .Where(x => x.BankAccount.BankAccountId == bankAccountId)
                .Include(x => x.BankAccount)
                .Include(x => x.BankAccount.QuotedCurrency)
                .Include(x => x.BankAccount.User);
        }

        public AccountValue GetLatestForBankAccount(int bankAccountId)
        {
            return _dbContext.AccountValues
                .Where(x => x.BankAccount.BankAccountId == bankAccountId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.BankAccount)
                .Include(x => x.BankAccount.QuotedCurrency)
                .Include(x => x.BankAccount.User)
                .FirstOrDefault();
        }

        public IEnumerable<AccountValue> GetAllForCashAccount(int bankAccountId)
        {
            return _dbContext.AccountValues
                .Where(x => x.CashAccount.CashAccountId == bankAccountId)
                .Include(x => x.CashAccount)
                .Include(x => x.CashAccount.QuotedCurrency)
                .Include(x => x.CashAccount.User);
        }

        public AccountValue GetLatestForCashAccount(int cashAccountId)
        {
            return _dbContext.AccountValues
                .Where(x => x.CashAccount.CashAccountId == cashAccountId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.CashAccount)
                .Include(x => x.CashAccount.QuotedCurrency)
                .Include(x => x.CashAccount.User)
                .FirstOrDefault();
        }

        public IEnumerable<AccountValue> GetAllForLoanAccount(int bankAccountId)
        {
            return _dbContext.AccountValues
                .Where(x => x.LoanAccount.LoanAccountId == bankAccountId)
                .Include(x => x.LoanAccount)
                .Include(x => x.LoanAccount.QuotedCurrency)
                .Include(x => x.LoanAccount.User);
        }

        public AccountValue GetLatestForLoanAccount(int loanAccountId)
        {
            return _dbContext.AccountValues
                .Where(x => x.LoanAccount.LoanAccountId == loanAccountId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.LoanAccount)
                .Include(x => x.LoanAccount.QuotedCurrency)
                .Include(x => x.LoanAccount.User)
                .FirstOrDefault();
        }

        public IEnumerable<AccountValue> GetAllForMortgageAccount(int bankAccountId)
        {
            return _dbContext.AccountValues
                .Where(x => x.MortgageAccount.MortgageAccountId == bankAccountId)
                .Include(x => x.MortgageAccount)
                .Include(x => x.MortgageAccount.QuotedCurrency)
                .Include(x => x.MortgageAccount.User);
        }

        public AccountValue GetLatestForMortgageAccount(int mortgageAccountId)
        {
            return _dbContext.AccountValues
                .Where(x => x.MortgageAccount.MortgageAccountId == mortgageAccountId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.MortgageAccount)
                .Include(x => x.MortgageAccount.QuotedCurrency)
                .Include(x => x.MortgageAccount.User)
                .FirstOrDefault();
        }

        public AccountValue GetById(int accountValueId)
        {
            return _dbContext.AccountValues
                .Include(x => x.BankAccount)
                .Include(x => x.BankAccount.QuotedCurrency)
                .Include(x => x.BankAccount.User)
                .Include(x => x.CashAccount)
                .Include(x => x.CashAccount.QuotedCurrency)
                .Include(x => x.CashAccount.User)
                .Include(x => x.LoanAccount)
                .Include(x => x.LoanAccount.QuotedCurrency)
                .Include(x => x.LoanAccount.User)
                .Include(x => x.MortgageAccount)
                .Include(x => x.MortgageAccount.QuotedCurrency)
                .Include(x => x.MortgageAccount.User)
                .SingleOrDefault(x => x.AccountValueId == accountValueId);
        }
    }
}
