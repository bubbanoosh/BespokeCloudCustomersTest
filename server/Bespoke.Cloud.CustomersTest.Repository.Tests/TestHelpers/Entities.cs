using Bespoke.Cloud.CustomersTest.API.Models;
using Bespoke.Cloud.CustomersTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bespoke.Cloud.CustomersTest.Repository.Tests.TestHelpers
{
    public class Entities
    {
        public static Customer GetTestCustomer()
        {
            return new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Aaaa",
                Email = "john@aaaa.com"
                
            };
        }

        public static IList<Customer> GetTestCustomersList()
        {
            return new List<Customer> {
                new Customer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Aaaa",
                    Email = "john@aaaa.com"
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Bbbb",
                    Email = "john@bbbb.com"
                },
                new Customer
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Cccc",
                    Email = "john@cccc.com"
                }
            };
        }

        public static IList<CustomerListDto> GetTestCustomersListDto()
        {
            return new List<CustomerListDto> {
                new CustomerListDto
                {
                    Id = 1,
                    Name = "John Aaaa"
                },
                new CustomerListDto
                {
                    Id = 2,
                    Name = "John Bbbb"
                },
                new CustomerListDto
                {
                    Id = 3,
                    Name = "John Cccc"
                }
            };
        }
    }
}
