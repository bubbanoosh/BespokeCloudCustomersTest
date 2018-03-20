using Bespoke.Cloud.CustomersTest.Entities;
using Bespoke.Cloud.CustomersTest.Repository.Interfaces;
using Dapper;
using DataManagement.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static System.Data.CommandType;

namespace Bespoke.Cloud.CustomersTest.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config) : base(config)
        {
            _config = config;

        }

        /// <summary>
        /// Register a new User to the Database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> Register(User user)
        {
            try
            {

                using (var connection = GetOpenConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@FirstName", user.FirstName);
                    parameters.Add("@LastName", user.LastName);
                    parameters.Add("@Email", user.Email);
                    parameters.Add("@PasswordHash", user.PasswordHash);
                    parameters.Add("@PasswordSalt", user.PasswordSalt);
                    await SqlMapper.ExecuteAsync(connection, "Users_Register", param: parameters, commandType: StoredProcedure);

                    int id = parameters.Get<int>("Id");

                    user.Id = id;

                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Completetly remove a User fron the Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveUser(int id)
        {
            using (var connection = GetOpenConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                SqlMapper.Execute(connection, "Users_DeleteUser", param: parameters, commandType: StoredProcedure);
                return true;
            }
        }

        /// <summary>
        /// Gets either the full list or performs a basic Name or email search
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IList<User> GetUsers(string searchText = "")
        {
            using (var connection = GetOpenConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SearchText", searchText);
                IList<User> userList = SqlMapper.Query<User>(connection, "Users_GetAllUsers", param: parameters, commandType: StoredProcedure).ToList();
                return userList;
            }
        }

        /// <summary>
        /// Gets a User by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                using (var connection = GetOpenConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Email", email);
                    return await SqlMapper.QueryFirstOrDefaultAsync<User>(connection, "Users_GetUserByUsername", param: parameters, commandType: StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            try
            {
                using (var connection = GetOpenConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    return SqlMapper.Query<User>(connection, "Users_GetUserById", param: parameters, commandType: StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(User user)
        {
            try
            {
                using (var connection = GetOpenConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", user.Id);
                    parameters.Add("@FirstName", user.FirstName);
                    parameters.Add("@LastName", user.LastName);
                    parameters.Add("@Email", user.Email);
                    SqlMapper.Execute(connection, "Users_UpdateUser", param: parameters, commandType: StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update a user password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        public bool UpdatePassword(int id, byte[] passwordHash, byte[] passwordSalt)
        {
            try
            {
                using (var connection = GetOpenConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    parameters.Add("@PasswordHash", passwordHash);
                    parameters.Add("@PasswordSalt", passwordSalt);
                    SqlMapper.Execute(connection, "Users_UpdatePassword", param: parameters, commandType: StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}