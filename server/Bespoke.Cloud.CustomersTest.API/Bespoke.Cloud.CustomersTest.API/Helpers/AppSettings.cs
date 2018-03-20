namespace Bespoke.Cloud.CustomersTest.API.Helpers
{
    public class AppSettings
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public int AuthenticationDays { get; set; }
        public bool UsingHttps { get; set; }
    }
}
