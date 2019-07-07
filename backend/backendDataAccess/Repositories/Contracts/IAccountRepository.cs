using backendData.Models;
using System.Collections.Generic;

namespace backendDataAccess.Repositories.Contracts
{
    public interface IAccountRepository
    {
        Account Add(Account account);
        void Delete(int accountId);
        IEnumerable<Account> GetAllForUser(int userId);
        Account GetById(int accountId);        
        Account Update(Account account);        
    }
}
