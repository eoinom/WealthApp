using backendData.Models;
using System.Collections.Generic;

namespace backendDataAccess.Repositories.Contracts
{
    public interface ICreditCardRepository
    {
        CreditCard GetById(int creditCardId);
        IEnumerable<CreditCard> GetAllForUser(int userId);
        CreditCard Add(CreditCard account);
        CreditCard Update(CreditCard account);
        void Delete(int creditCardId);
    }
}
