using Bespoke.Cloud.CustomersTest.Entities;
using System.Collections.Generic;

namespace Bespoke.Cloud.CustomersTest.Business.Interfaces
{
    public interface ICustomerManager
    {
        int AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool RemoveCustomer(int id);
        IList<Customer> GetCustomers(string searchText);
        Customer GetCustomerById(int id);
    }
}
