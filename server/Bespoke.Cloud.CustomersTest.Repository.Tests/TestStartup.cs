using Bespoke.Cloud.CustomersTest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class TestStartup : Startup
{
    public TestStartup(IConfiguration configuration) : base(configuration)
    {

    }

    public void ConfigureServices(IServiceCollection services)
    {

    }
}