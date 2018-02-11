using Bespoke.Cloud.CustomersTest.Business.Interfaces;
using Bespoke.Cloud.CustomersTest.Entities;
using Bespoke.Cloud.CustomersTest.Repository.Interfaces;
using System.Collections.Generic;

namespace Bespoke.Cloud.CustomersTest.Business
{
    public class CustomerManager: ICustomerManager
    {
        ICustomerRepository _userRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        public CustomerManager(ICustomerRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int AddCustomer(Customer user)
        {
            return _userRepository.AddCustomer(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveCustomer(int id)
        {
            return _userRepository.RemoveCustomer(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomers(string searchText = "")
        {
            return _userRepository.GetCustomers(searchText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Customer GetCustomerById(int userId)
        {
            return _userRepository.GetCustomerById(userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateCustomer(Customer user)
        {
            return _userRepository.UpdateCustomer(user);
        }
    }
}
