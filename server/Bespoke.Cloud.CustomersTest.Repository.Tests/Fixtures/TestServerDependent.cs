using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;

public class TestServerDependent : IClassFixture<TestServerFixture>
{
    private readonly TestServerFixture _fixture;
    public TestServer TestServer => _fixture.Server;
    public HttpClient Client => _fixture.Client;

    public TestServerDependent(TestServerFixture fixture)
    {
        _fixture = fixture;
    }

    protected TService GetService<TService>()
        where TService : class
    {
        return _fixture.GetService<TService>();
    }
}