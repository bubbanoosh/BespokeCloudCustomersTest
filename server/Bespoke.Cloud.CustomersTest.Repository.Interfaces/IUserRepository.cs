using Bespoke.Cloud.CustomersTest.Entities;
using System.Collections.Generic;

namespace Bespoke.Cloud.CustomersTest.Repository.Interfaces
{
    public interface IUserRepository
    {
        User Register(User user);
        bool UpdateUser(User user);
        bool UpdatePassword(int id, byte[] passwordHash, byte[] passwordSalt);
        bool RemoveUser(int id);
        IList<User> GetUsers(string searchText);
        User GetUserByEmail(string email);
        User GetUserById(int id);
    }
}
