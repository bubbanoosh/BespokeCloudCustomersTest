using Bespoke.Cloud.CustomersTest.Business.Interfaces;
using Bespoke.Cloud.CustomersTest.Entities;
using Bespoke.Cloud.CustomersTest.Repository.Interfaces;
using System.Collections.Generic;

namespace Bespoke.Cloud.CustomersTest.Business.Customers
{
    public class CustomersManager: ICustomerManager
    {
        ICustomerRepository _customerRepository;

        public CustomersManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Add a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public int AddCustomer(Customer customer)
        {
            return _customerRepository.AddCustomer(customer);
        }

        /// <summary>
        /// Remove customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveCustomer(int id)
        {
            return _customerRepository.RemoveCustomer(id);
        }

        /// <summary>
        /// Get all or search customers
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomers(string searchText = "")
        {
            return _customerRepository.GetCustomers(searchText);
        }

        /// <summary>
        /// Get a customer by id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerById(int customerId)
        {
            return _customerRepository.GetCustomerById(customerId);
        }

        /// <summary>
        /// Update a customer (PUT not PATCH)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool UpdateCustomer(Customer customer)
        {
            return _customerRepository.UpdateCustomer(customer);
        }
    }
}
