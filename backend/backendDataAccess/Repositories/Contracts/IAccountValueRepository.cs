using backendData.Models;
using System.Collections.Generic;

namespace backendDataAccess.Repositories.Contracts
{
    public interface IAccountValueRepository
    {
        AccountValue GetById(int id);
        IEnumerable<AccountValue> GetAllForBankAccount(int accountId);
        IEnumerable<AccountValue> GetAllForCashAccount(int accountId);
        IEnumerable<AccountValue> GetAllForLoanAccount(int accountId);
        IEnumerable<AccountValue> GetAllForMortgageAccount(int accountId);
        AccountValue Add(AccountValue accountValue);
    }
}
