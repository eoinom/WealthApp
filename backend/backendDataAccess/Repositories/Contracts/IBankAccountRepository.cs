using backendData.Models;
using System.Collections.Generic;

namespace backendDataAccess.Repositories.Contracts
{
    public interface IBankAccountRepository
    {        
        BankAccount GetById(int id);
        IEnumerable<BankAccount> GetAllForUser(int userId);
        BankAccount Add(BankAccount bankAccount);
        BankAccount Update(BankAccount bankAccount);
    }
}
