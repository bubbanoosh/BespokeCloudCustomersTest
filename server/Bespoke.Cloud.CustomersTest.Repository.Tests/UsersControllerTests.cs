using Bespoke.Cloud.CustomersTest.Entities;
using Moq;
using Xunit;
using Bespoke.Cloud.CustomersTest.Business.Interfaces;
using Bespoke.Cloud.CustomersTest.API.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Bespoke.Cloud.CustomersTest.API.Helpers;
using Bespoke.Cloud.CustomersTest.API.Dtos;
using System.Collections.Generic;
using AutoMapper;
using Bespoke.Cloud.CustomersTest.Repository.Tests.EqualityComparers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Bespoke.Cloud.CustomersTest.Repository.Tests.Fixtures;

namespace Bespoke.Cloud.CustomersTest.Repository.Tests
{
    public class UsersControllerTests //: IClassFixture<UsersFixture>
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        UsersFixture _usersFixture;

        public UsersControllerTests()//UsersFixture usersFixture)
        {
            //_usersFixture = usersFixture;


            var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(@"../../../"))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration));
            _client = _server.CreateClient();


        }

        [Fact(DisplayName = "UsersController: UnAuthorisedAccess to Users list")]
        public async Task UnAuthorisedAccess()
        {
            var response = await _client.GetAsync("/api/users");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact(DisplayName = "UsersController: Authenticate and get Token")]
        public async Task AuthenticateUser()
        {
            var bodyString = @"{email: ""e@bubbanoosh.com.au"", password: ""N00sh1970""}";
            var response = await _client.PostAsync("/api/users/login", new StringContent(bodyString, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            Assert.NotNull((string)responseJson["token"]);
        }

        [Fact(DisplayName = "UsersController: GetUsers with Authorisation")]
        public async Task GetUsersList()
        {
            var bodyString = @"{username: ""e@bubbanoosh.com.au"", password: ""N00sh1970""}";
            var response = await _client.PostAsync("/api/users", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/users");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var usersResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, usersResponse.StatusCode);

            var userResponseString = await usersResponse.Content.ReadAsStringAsync();
            var userResponseJson = JArray.Parse(userResponseString);
            Assert.True(userResponseJson.Count == 4);
        }
    }
}
