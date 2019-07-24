using backendData;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace backendDataAccess.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly backendDbContext _dbContext;

        public LoanRepository(backendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Loan Add(Loan loan)
        {
            User user = _dbContext.Users
                            .Include(x => x.Country)
                            .Include(x => x.DisplayCurrency)
                            .SingleOrDefault(x => x.UserId == loan.User.UserId);
            loan.User = user;

            Currency currency = _dbContext.Currencies.SingleOrDefault(x => x.Code == loan.QuotedCurrency.Code);
            loan.QuotedCurrency = currency;

            addLoanAsync(loan);
            _dbContext.SaveChanges();
            _dbContext.Entry<Loan>(loan).State = EntityState.Detached;
            _dbContext.Entry<Currency>(loan.QuotedCurrency).State = EntityState.Detached;
            _dbContext.Entry<User>(loan.User).State = EntityState.Detached;
            _dbContext.Entry<Currency>(loan.User.DisplayCurrency).State = EntityState.Detached;
            _dbContext.SaveChanges();
            return loan;
        }

        async public void addLoanAsync(Loan loan)
        {
            await _dbContext.Loans.AddAsync(loan);
        }

        public void Delete(int loanId)
        {
            var loan = _dbContext.Loans
                                .Include(x => x.LoanValues)
                                .SingleOrDefault(x => x.LoanId == loanId);
            
            _dbContext.Remove(loan);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Loan> GetAllForUser(int userId)
        {
            return _dbContext.Loans.AsNoTracking()
                .Where(x => x.User.UserId == userId)
                .Include(x => x.QuotedCurrency)
                .Include(x => x.User)
                    .ThenInclude(u => u.Country)
                .Include(x => x.User)
                    .ThenInclude(u => u.DisplayCurrency)
                .Include(x => x.LoanValues);
        }

        public Loan GetById(int loanId)
        {
            return _dbContext.Loans.AsNoTracking()
                .Include(x => x.QuotedCurrency)
                .Include(x => x.User)
                    .ThenInclude(u => u.Country)
                .Include(x => x.User)
                    .ThenInclude(u => u.DisplayCurrency)
                .Include(x => x.LoanValues)
                .SingleOrDefault(x => x.LoanId == loanId);
        }

        public Loan Update(Loan loanUpdates)
        {
            //User user = _dbContext.Users.SingleOrDefault(x => x.UserId == loanUpdates.User.UserId);
            //loanUpdates.User = user;

            Currency currency = _dbContext.Currencies.FirstOrDefault(x => x.Code == loanUpdates.QuotedCurrency.Code);
            loanUpdates.QuotedCurrency = currency;
            
            var loan = _dbContext.Loans
                .Include(x => x.QuotedCurrency)
                .Include(x => x.User)
                    .ThenInclude(u => u.Country)
                .Include(x => x.User)
                    .ThenInclude(u => u.DisplayCurrency)
                .Include(x => x.LoanValues)
                .SingleOrDefault(x => x.LoanId == loanUpdates.LoanId && x.User.UserId == loanUpdates.User.UserId);

            if (loan != null)
            {
                loan.LoanName = loanUpdates.LoanName;
                loan.Description = loanUpdates.Description;                
                loan.Institution = loanUpdates.Institution;
                loan.IsActive = loanUpdates.IsActive;
                loan.Type = loanUpdates.Type;
                loan.StartPrincipal = loanUpdates.StartPrincipal;
                loan.RateType = loanUpdates.RateType;
                loan.TotalTerm = loanUpdates.TotalTerm;
                loan.FixedTerm = loanUpdates.FixedTerm;
                loan.AprRate = loanUpdates.AprRate;
                loan.StartDate = loanUpdates.StartDate;
                loan.QuotedCurrency = loanUpdates.QuotedCurrency;

                _dbContext.SaveChanges();

                //Note: need to detach to avoid tracking error when trying to update the same entry again with the same context
                //See: https://entityframeworkcore.com/knowledge-base/50987635/the-instance-of-entity-type--item--cannot-be-tracked-because-another-instance-with-the-same-key-value-for---id---is-already-being-tracked
                _dbContext.Entry<Loan>(loan).State = EntityState.Detached;
                _dbContext.Entry<Currency>(loan.QuotedCurrency).State = EntityState.Detached;
                _dbContext.Entry<User>(loan.User).State = EntityState.Detached;
                _dbContext.Entry<Currency>(loan.User.DisplayCurrency).State = EntityState.Detached;
                _dbContext.SaveChanges();
            }

            return loan;
        }
    }
}
