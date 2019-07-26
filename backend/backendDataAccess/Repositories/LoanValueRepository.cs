using backendData;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace backendDataAccess.Repositories
{
    public class LoanValueRepository : ILoanValueRepository
    {
        private readonly backendDbContext _dbContext;

        public LoanValueRepository(backendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public LoanValue Add(LoanValue value)
        {
            if (value.LoanValueId > 0 && _dbContext.LoanValues.Any(x => x.LoanValueId == value.LoanValueId))
            {
                return null;
            }

            if (value.Loan != null)
            {
                Loan loan = _dbContext.Loans
                                            //.Include(x => x.LoanValues)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.LoanId == value.Loan.LoanId);
                value.Loan = loan;
            }

            _dbContext.LoanValues.Add(value);
            _dbContext.SaveChanges();
            _dbContext.Entry<LoanValue>(value).State = EntityState.Detached;
            _dbContext.Entry<Loan>(value.Loan).State = EntityState.Detached;
            _dbContext.Entry<Currency>(value.Loan.QuotedCurrency).State = EntityState.Detached;
            _dbContext.SaveChanges();
            return value;
        }

        public void DeleteById(int valueId)
        {
            var value = _dbContext.LoanValues.SingleOrDefault(x => x.LoanValueId == valueId);
            _dbContext.Remove(value);
            _dbContext.SaveChanges();
        }

        public void DeleteByIds(int[] valueIds)
        {
            foreach (var valueId in valueIds)
            {
                var value = _dbContext.LoanValues.SingleOrDefault(x => x.LoanValueId == valueId);
                _dbContext.Remove(value);
            }           
            _dbContext.SaveChanges();
        }

        public IEnumerable<LoanValue> GetAllForLoan(int loanId)
        {
            return _dbContext.LoanValues.AsNoTracking()
                .Where(x => x.Loan.LoanId == loanId)
                .Include(x => x.Loan)
                .Include(x => x.Loan.QuotedCurrency)
                .Include(x => x.Loan.User);
        }

        public LoanValue GetLatestForLoan(int loanId)
        {
            return _dbContext.LoanValues.AsNoTracking()
                .Where(x => x.Loan.LoanId == loanId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.Loan)
                .Include(x => x.Loan.QuotedCurrency)
                .Include(x => x.Loan.User)
                .FirstOrDefault();
        }

        public LoanValue GetById(int valueId)
        {
            return _dbContext.LoanValues.AsNoTracking()
                .Include(x => x.Loan)
                .Include(x => x.Loan.QuotedCurrency)
                .Include(x => x.Loan.User)
                .SingleOrDefault(x => x.LoanValueId == valueId);
        }

        public LoanValue Update(LoanValue valueUpdates)
        {
            if (valueUpdates.Loan != null)
            {
                valueUpdates.Loan = _dbContext.Loans.AsNoTracking()
                                        .Include(x => x.QuotedCurrency)
                                        .SingleOrDefault(x => x.LoanId == valueUpdates.Loan.LoanId);
            }

            var loanValue = _dbContext.LoanValues
                .Include(x => x.Loan)
                    .ThenInclude(u => u.QuotedCurrency)
                .Include(x => x.Loan)
                    .ThenInclude(u => u.User)
                    .ThenInclude(u => u.DisplayCurrency)
                .SingleOrDefault(x => x.LoanValueId == valueUpdates.LoanValueId && x.Loan.LoanId == valueUpdates.Loan.LoanId);

            if (loanValue != null)
            {
                loanValue.Date = valueUpdates.Date;
                loanValue.Value = valueUpdates.Value;
                loanValue.RateToUserCurrency = valueUpdates.RateToUserCurrency;
                loanValue.ValueUserCurrency = valueUpdates.ValueUserCurrency;

                _dbContext.SaveChanges();

                //Note: need to detach to avoid tracking error when trying to update the same entry again with the same context
                //See: https://entityframeworkcore.com/knowledge-base/50987635/the-instance-of-entity-type--item--cannot-be-tracked-because-another-instance-with-the-same-key-value-for---id---is-already-being-tracked
                _dbContext.Entry<LoanValue>(loanValue).State = EntityState.Detached;
                _dbContext.Entry<Loan>(loanValue.Loan).State = EntityState.Detached;
                _dbContext.Entry<Currency>(loanValue.Loan.QuotedCurrency).State = EntityState.Detached;
                _dbContext.Entry<User>(loanValue.Loan.User).State = EntityState.Detached;
                _dbContext.Entry<Currency>(loanValue.Loan.User.DisplayCurrency).State = EntityState.Detached;
                _dbContext.SaveChanges();
            }
            return loanValue;
        }
    }
}
