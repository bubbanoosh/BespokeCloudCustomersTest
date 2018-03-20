using Bespoke.Cloud.CustomersTest.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bespoke.Cloud.CustomersTest.Business.Interfaces
{
    public interface IUserManager
    {
        Task<User> Register(User user, string password);
        bool UpdateUser(User user);
        bool UpdatePassword(int id, string password);
        bool RemoveUser(int id);
        IList<User> GetUsers(string searchText);
        Task<User> GetUserByEmail(string email);
        User GetUserById(int id);
        Task<User> Authenticate(string username, string password);
    }
}
