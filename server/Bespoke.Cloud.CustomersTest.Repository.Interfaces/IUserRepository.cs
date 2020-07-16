using Bespoke.Cloud.CustomersTest.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bespoke.Cloud.CustomersTest.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Register(User user);
        bool UpdateUser(User user);
        bool UpdatePassword(int id, byte[] passwordHash, byte[] passwordSalt);
        bool RemoveUser(int id);
        Task<IList<User>> GetUsers(string searchText);
        Task<User> GetUserByEmail(string email);
        User GetUserById(int id);
    }
}
