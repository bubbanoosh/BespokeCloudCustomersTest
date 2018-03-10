using Bespoke.Cloud.CustomersTest.Entities;
using System.Collections.Generic;

namespace Bespoke.Cloud.CustomersTest.Business.Interfaces
{
    public interface IUserManager
    {
        User Register(User user, string password);
        bool UpdateUser(User user);
        bool UpdatePassword(int id, string password);
        bool RemoveUser(int id);
        IList<User> GetUsers(string searchText);
        User GetUserByEmail(string email);
        User GetUserById(int id);
        User Authenticate(string username, string password);
    }
}
