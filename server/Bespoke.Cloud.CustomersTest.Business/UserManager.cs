using Bespoke.Cloud.CustomersTest.Business.Interfaces;
using Bespoke.Cloud.CustomersTest.Entities;
using Bespoke.Cloud.CustomersTest.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace Bespoke.Cloud.CustomersTest.Business
{
    public class UserManager : IUserManager
    {
        IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Register a new User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Register(User user, string password)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(password))
                return null;

            if (GetUserByEmail(user.Email) != null)
                throw new ApplicationException($"User {user.Email} Already exists!");

            // Generate the Password Hash & Salt into a Tuple
            var passwordHashTuple = GeneratePassword(password);
            user.PasswordHash = passwordHashTuple.h;
            user.PasswordSalt = passwordHashTuple.s;

            return _userRepository.Register(user);
        }

        /// <summary>
        /// Remove user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveUser(int id)
        {
            return _userRepository.RemoveUser(id);
        }

        /// <summary>
        /// Get all or search users
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IList<User> GetUsers(string searchText = "")
        {
            return _userRepository.GetUsers(searchText);
        }

        /// <summary>
        /// Get a User by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ApplicationException("Cannot get User. No email supplied?");

            return _userRepository.GetUserByEmail(email);
        }

        /// <summary>
        /// Get a User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></return>
        public User GetUserById(int id)
        {
            if (id < 1)
                throw new ApplicationException("Could not retrieve by invalid Id");

            return _userRepository.GetUserById(id);
        }

        /// <summary>
        /// Updates a User Password only
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UpdatePassword(int id, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Password must be supplied!");

            var passwordHashTuple = GeneratePassword(password);

            return _userRepository.UpdatePassword(id, passwordHashTuple.h, passwordHashTuple.s);
        }

        /// <summary>
        /// Updates a User (Not password)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(User userUpdate)
        {
            if (userUpdate == null)
                throw new ArgumentNullException("User");

            var user = GetUserById(userUpdate.Id);
            if (user == null)
                throw new ApplicationException("Cannot find user to update");

            if (userUpdate.Email != user.Email)
            {
                if (GetUserByEmail(userUpdate.Email) != null)
                {
                    throw new ApplicationException($"User {user.Email} Already exists!");
                }
            }

            return _userRepository.UpdateUser(userUpdate);
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Authenticate(string username, string password)
        {
            var user = new User { Id = 1, FirstName = "Errol", LastName = "Willy", Email = "e@bubbanoosh.com.au" };


            return user;
        }



        #region private helper methods
        /// <summary>
        /// Verify the users password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("String value cannot be empty or whitespace.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static (byte[] h, byte[] s) GeneratePassword(string password)
        {

            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            byte[] passwordHash, passwordSalt;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return (passwordHash, passwordSalt);
        }
        #endregion

    }
}
