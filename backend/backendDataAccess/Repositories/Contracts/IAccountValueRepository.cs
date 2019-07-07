using backendData.Models;
using System.Collections.Generic;

namespace backendDataAccess.Repositories.Contracts
{
    public interface IAccountValueRepository
    {
        AccountValue Add(AccountValue value);
        void DeleteById(int valueId);
        void DeleteByIds(int[] valueIds);
        AccountValue GetById(int id);
        IEnumerable<AccountValue> GetAllForAccount(int accountId);
        AccountValue Update(AccountValue value);
    }
}
