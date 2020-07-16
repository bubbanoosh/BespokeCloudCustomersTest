using Bespoke.Cloud.CustomersTest.API.Dtos;
using Bespoke.Cloud.CustomersTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bespoke.Cloud.CustomersTest.Repository.Tests.TestHelpers
{
    public class Entities
    {
        public static CustomerDetailDto GetTestCustomerDetailDto()
        {
            return new CustomerDetailDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Aaaa",
                Email = "john@aaaa.com"

            };
        }

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

        public static User GetTestUser()
        {
            return new User
            {
                Id = 1,
                DateCreated = new DateTime(2018, 03, 14),
                FirstName = "John",
                LastName = "Aaaa",
                Email = "john@aaaa.com",
                PasswordHash = Encoding.ASCII.GetBytes("0x15EF46763F0FF966D8099F4F9E78EE4C45EB722F9CEBD5D677C5D23B12832B66F7B5F47C2F7A2687CEFDD659EA157467B0F858F0D8D8A03C32C1FBD8848E1F58"),
                PasswordSalt = Encoding.ASCII.GetBytes("0x6F40A86A7C663EE399B3959A4285833DD1A87BDFFCFB1F02ED4C5B776C70AD6CF5A386C6048EF4152D2AF82AE1C63B515FCF21C51590938AFE08C2002D39F13F01A7FD91136FE151D47BF0C8DD25CA97C31B9611B61D13BFBF27A309B323D9AA9EBFD65861E8559A015EE48230FF0BE6CAC48E262D6471681005DB6E81016555")
            };
        }


    }
}
