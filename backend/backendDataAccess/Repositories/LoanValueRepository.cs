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
            if (value.Loan != null)
            {
                Loan bankLoan = _dbContext.Loans
                                            .Include(x => x.LoanValues)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.LoanId == value.Loan.LoanId);
                value.Loan = bankLoan;
            }

            _dbContext.LoanValues.Add(value);
            _dbContext.SaveChanges();
            _dbContext.Entry<LoanValue>(value).State = EntityState.Detached;
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

        public LoanValue Update(LoanValue value)
        {
            if (value.Loan != null)
            {
                Loan bankLoan = _dbContext.Loans.AsNoTracking()
                                            //.Include(x => x.LoanValues).AsNoTracking()
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.LoanId == value.Loan.LoanId);
                value.Loan = bankLoan;
                _dbContext.Entry<Loan>(bankLoan).State = EntityState.Detached;
                _dbContext.SaveChanges();
            }

            _dbContext.LoanValues.Update(value);
            _dbContext.SaveChanges();

            //Note: need to detach to avoid tracking error when trying to update the same entry again with the same context
            //See: https://entityframeworkcore.com/knowledge-base/50987635/the-instance-of-entity-type--item--cannot-be-tracked-because-another-instance-with-the-same-key-value-for---id---is-already-being-tracked
            _dbContext.Entry<LoanValue>(value).State = EntityState.Detached;
            _dbContext.SaveChanges();

            return value;
        }
    }
}
