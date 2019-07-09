using backendData.Models;
using System.Collections.Generic;

namespace backendDataAccess.Repositories.Contracts
{
    public interface ILoanValueRepository
    {
        LoanValue Add(LoanValue value);
        void DeleteById(int valueId);
        void DeleteByIds(int[] valueIds);
        LoanValue GetById(int id);
        IEnumerable<LoanValue> GetAllForLoan(int loanId);
        LoanValue Update(LoanValue value);
    }
}
