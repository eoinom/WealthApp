using backendData.Models;
using System.Collections.Generic;

namespace backendDataAccess.Repositories.Contracts
{
    public interface ILoanRepository
    {
        Loan Add(Loan loan);
        void Delete(int loanId);
        IEnumerable<Loan> GetAllForUser(int userId);
        Loan GetById(int loanId);
        Loan Update(Loan loan);        
    }
}
