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
            if (accountValue.BankAccount != null)
            {
                BankAccount bankAccount = _dbContext.BankAccounts
                                            .Include(x => x.AccountValues)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.BankAccountId == accountValue.BankAccount.BankAccountId);
                accountValue.BankAccount = bankAccount;
            }

            if (accountValue.CashAccount != null)
            {
                CashAccount cashAccount = _dbContext.CashAccounts
                                            .Include(x => x.AccountValues)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.CashAccountId == accountValue.CashAccount.CashAccountId);
                accountValue.CashAccount = cashAccount;
            }

            if (accountValue.LoanAccount != null)
            {
                LoanAccount loanAccount = _dbContext.LoanAccounts
                                            .Include(x => x.BalancesOwing)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.LoanAccountId == accountValue.LoanAccount.LoanAccountId);
                accountValue.LoanAccount = loanAccount;
            }

            if (accountValue.MortgageAccount != null)
            {
                MortgageAccount mortgageAccount = _dbContext.MortgageAccounts
                                            .Include(x => x.BalancesOwing)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.MortgageAccountId == accountValue.MortgageAccount.MortgageAccountId);
                accountValue.MortgageAccount = mortgageAccount;
            }

            _dbContext.AccountValues.Add(accountValue);
            _dbContext.SaveChanges();
            _dbContext.Entry<AccountValue>(accountValue).State = EntityState.Detached;
            return accountValue;
        }

        public AccountValue Update(AccountValue accountValue)
        {
            if (accountValue.BankAccount != null)
            {
                BankAccount bankAccount = _dbContext.BankAccounts
                                            .Include(x => x.AccountValues)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.BankAccountId == accountValue.BankAccount.BankAccountId);
                accountValue.BankAccount = bankAccount;
            }

            if (accountValue.CashAccount != null)
            {
                CashAccount cashAccount = _dbContext.CashAccounts
                                            .Include(x => x.AccountValues)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.CashAccountId == accountValue.CashAccount.CashAccountId);
                accountValue.CashAccount = cashAccount;
            }

            if (accountValue.LoanAccount != null)
            {
                LoanAccount loanAccount = _dbContext.LoanAccounts
                                            .Include(x => x.BalancesOwing)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.LoanAccountId == accountValue.LoanAccount.LoanAccountId);
                accountValue.LoanAccount = loanAccount;
            }

            if (accountValue.MortgageAccount != null)
            {
                MortgageAccount mortgageAccount = _dbContext.MortgageAccounts
                                            .Include(x => x.BalancesOwing)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.MortgageAccountId == accountValue.MortgageAccount.MortgageAccountId);
                accountValue.MortgageAccount = mortgageAccount;
            }

            _dbContext.AccountValues.Update(accountValue);
            _dbContext.SaveChanges();

            //Note: need to detach to avoid tracking error when trying to update the same entry again with the same context
            //See: https://entityframeworkcore.com/knowledge-base/50987635/the-instance-of-entity-type--item--cannot-be-tracked-because-another-instance-with-the-same-key-value-for---id---is-already-being-tracked
            _dbContext.Entry<AccountValue>(accountValue).State = EntityState.Detached;

            return accountValue;
        }

        public void DeleteById(int accountValueId)
        {
            var accountValue = _dbContext.AccountValues.SingleOrDefault(x => x.AccountValueId == accountValueId);
            _dbContext.Remove(accountValue);
            _dbContext.SaveChanges();
        }

        public void DeleteByIds(int[] accountValueIds)
        {
            foreach (var accountValueId in accountValueIds)
            {
                var accountValue = _dbContext.AccountValues.SingleOrDefault(x => x.AccountValueId == accountValueId);
                _dbContext.Remove(accountValue);
            }           
            _dbContext.SaveChanges();
        }

        public IEnumerable<AccountValue> GetAllForBankAccount(int bankAccountId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Where(x => x.BankAccount.BankAccountId == bankAccountId)
                .Include(x => x.BankAccount)
                .Include(x => x.BankAccount.QuotedCurrency)
                .Include(x => x.BankAccount.User);
        }

        public AccountValue GetLatestForBankAccount(int bankAccountId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Where(x => x.BankAccount.BankAccountId == bankAccountId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.BankAccount)
                .Include(x => x.BankAccount.QuotedCurrency)
                .Include(x => x.BankAccount.User)
                .FirstOrDefault();
        }

        public IEnumerable<AccountValue> GetAllForCashAccount(int bankAccountId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Where(x => x.CashAccount.CashAccountId == bankAccountId)
                .Include(x => x.CashAccount)
                .Include(x => x.CashAccount.QuotedCurrency)
                .Include(x => x.CashAccount.User);
        }

        public AccountValue GetLatestForCashAccount(int cashAccountId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Where(x => x.CashAccount.CashAccountId == cashAccountId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.CashAccount)
                .Include(x => x.CashAccount.QuotedCurrency)
                .Include(x => x.CashAccount.User)
                .FirstOrDefault();
        }

        public IEnumerable<AccountValue> GetAllForLoanAccount(int bankAccountId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Where(x => x.LoanAccount.LoanAccountId == bankAccountId)
                .Include(x => x.LoanAccount)
                .Include(x => x.LoanAccount.QuotedCurrency)
                .Include(x => x.LoanAccount.User);
        }

        public AccountValue GetLatestForLoanAccount(int loanAccountId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Where(x => x.LoanAccount.LoanAccountId == loanAccountId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.LoanAccount)
                .Include(x => x.LoanAccount.QuotedCurrency)
                .Include(x => x.LoanAccount.User)
                .FirstOrDefault();
        }

        public IEnumerable<AccountValue> GetAllForMortgageAccount(int bankAccountId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Where(x => x.MortgageAccount.MortgageAccountId == bankAccountId)
                .Include(x => x.MortgageAccount)
                .Include(x => x.MortgageAccount.QuotedCurrency)
                .Include(x => x.MortgageAccount.User);
        }

        public AccountValue GetLatestForMortgageAccount(int mortgageAccountId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Where(x => x.MortgageAccount.MortgageAccountId == mortgageAccountId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.MortgageAccount)
                .Include(x => x.MortgageAccount.QuotedCurrency)
                .Include(x => x.MortgageAccount.User)
                .FirstOrDefault();
        }

        public AccountValue GetById(int accountValueId)
        {
            return _dbContext.AccountValues.AsNoTracking()
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
