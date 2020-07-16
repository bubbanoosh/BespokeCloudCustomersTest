using System;

namespace Bespoke.Cloud.CustomersTest.Business.Helpers
{
    public static class StringExtensions
    {
        public static string ToNullOrEmptyString(this String text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            return text.Trim();
        }
    }
}
