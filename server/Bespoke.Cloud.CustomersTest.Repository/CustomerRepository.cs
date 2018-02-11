using Bespoke.Cloud.CustomersTest.Entities;
using Bespoke.Cloud.CustomersTest.Repository.Interfaces;
using Dapper;
using DataManagement.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static System.Data.CommandType;

namespace Bespoke.Cloud.CustomersTest.Repository
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        private readonly IConfiguration _config;

        public CustomerRepository(IConfiguration config) : base(config)
        {
            _config = config;

        }

        /// <summary>
        /// Add a new Customer to the Database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public int AddCustomer(Customer customer)
        {
            try
            {

                using (var connection = GetOpenConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@FirstName", customer.FirstName);
                    parameters.Add("@LastName", customer.LastName);
                    parameters.Add("@Email", customer.Email);
                    SqlMapper.Execute(connection, "Customers_AddCustomer", param: parameters, commandType: StoredProcedure);

                    int id = parameters.Get<int>("Id");

                    return id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Completetly remove a Customer fron the Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveCustomer(int id)
        {
            using (var connection = GetOpenConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                SqlMapper.Execute(connection, "Customers_DeleteCustomer", param: parameters, commandType: StoredProcedure);
                return true;
            }
        }

        /// <summary>
        /// Gets either the full list or performs a basic Name or email search
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomers(string searchText = "")
        {
            using (var connection = GetOpenConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SearchText", searchText);
                IList<Customer> customerList = SqlMapper.Query<Customer>(connection, "Customers_GetAllCustomers", param: parameters, commandType: StoredProcedure).ToList();
                return customerList;
            }
        }

        /// <summary>
        /// Gets a Customer by Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerById(int customerId)
        {
            try
            {
                using (var connection = GetOpenConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", customerId);
                    return SqlMapper.Query<Customer>(connection, "Customers_GetCustomerById", param: parameters, commandType: StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                using (var connection = GetOpenConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", customer.Id);
                    parameters.Add("@FirstName", customer.FirstName);
                    parameters.Add("@LastName", customer.LastName);
                    parameters.Add("@Email", customer.Email);
                    SqlMapper.Execute(connection, "Customers_UpdateCustomer", param: parameters, commandType: StoredProcedure);
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