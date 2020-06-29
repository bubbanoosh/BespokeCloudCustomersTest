using System;
using System.Net.Http;
using AutoMapper;
using Bespoke.Cloud.CustomersTest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

public class TestServerFixture : IDisposable
{
    public TestServer Server { get; }
    public HttpClient Client { get; }

    public TestServerFixture()
    {
        // UseStaticRegistration is needed to workaround AutoMapper double initialization. Remove if you don't use AutoMapper.
        ServiceCollectionExtensions.UseStaticRegistration = false;

        var hostBuilder = new WebHostBuilder()
            .UseEnvironment("Development")
            .UseStartup<TestStartup>();

        Server = new TestServer(hostBuilder);
        Client = Server.CreateClient();
    }

    public void Dispose()
    {
        Server.Dispose();
        Client.Dispose();
    }

    public TService GetService<TService>()
        where TService : class
    {
        return Server?.Host?.Services?.GetService(typeof(TService)) as TService;
    }
}