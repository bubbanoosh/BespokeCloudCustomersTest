using Bespoke.Cloud.CustomersTest.API.Models;
using System;
using System.Collections.Generic;

namespace Bespoke.Cloud.CustomersTest.Repository.Tests.EqualityComparers
{
    public class CustomersEqualityComparer : IEqualityComparer<CustomerListDto>
    {
        public bool Equals(CustomerListDto x, CustomerListDto y)
        {
            return (Int32.Equals(x.Id, y.Id) == true) &&
                   (string.CompareOrdinal(x.Name, y.Name) == 0);
        }

        public int GetHashCode(CustomerListDto obj)
        {
            return obj.Id.GetHashCode() ^ obj.Name.GetHashCode();
        }
    }
}
