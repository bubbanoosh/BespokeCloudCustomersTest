using System;
using System.Globalization;

namespace Bespoke.Cloud.CustomersTest.API.Helpers
{
    // Custom exception class for throwing application specific exceptions (e.g. for validation) 
    // that can be caught and handled within the application
    public class ApplicationException : Exception
    {
        public ApplicationException() : base() { }

        public ApplicationException(string message) : base(message) { }

        public ApplicationException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
