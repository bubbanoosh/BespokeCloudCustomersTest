using Bespoke.Cloud.CustomersTest.Entities;
using System.Collections.Generic;

namespace Bespoke.Cloud.CustomersTest.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        int AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool RemoveCustomer(int id);
        IList<Customer> GetCustomers(string searchText);
        Customer GetCustomerById(int id);
    }
}
