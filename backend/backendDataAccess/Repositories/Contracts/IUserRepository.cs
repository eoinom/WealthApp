using backendData.Models;
using System.Collections.Generic;

namespace backendDataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Add(User user);
        User Login(string email, string password);
        bool CheckCredentials(string email, string password);
    }
}
