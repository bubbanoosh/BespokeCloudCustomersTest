using Xunit;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Bespoke.Cloud.CustomersTest.Business.Interfaces;
using Moq;
using Bespoke.Cloud.CustomersTest.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Bespoke.Cloud.CustomersTest.API.Controllers;
using AutoMapper;
using Microsoft.Extensions.Options;
using Bespoke.Cloud.CustomersTest.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using Bespoke.Cloud.CustomersTest.API.Dtos;
using FluentAssertions;

namespace Bespoke.Cloud.CustomersTest.Repository.Tests
{
    public class UsersControllerTests
    {
        private UsersController SetUpUsersController(Mock<IUserManager> mockUserManager)
        {
            var mockILogger = new Mock<ILogger<UsersController>>();
            var mockIMapper = new Mock<IMapper>();
            var mockIOptions = new Mock<IOptions<AppSettings>>();

            // var services = Mock.Of<IOptions<AppSettings>>(ap => ap).;

            var usersController = new UsersController(mockUserManager.Object, mockILogger.Object
                , mockIMapper.Object, mockIOptions.Object);

            return usersController;
        }

        [Fact(DisplayName = "UsersController: Get Users has UnAuthorizedAttribute")]
        public void GetUsersHasUnAuthorizedAttribute()
        {
            var mockUserManager = new Mock<IUserManager>();
            mockUserManager
                .Setup(x => x.GetUsers(string.Empty))
                .ReturnsAsync((IList<User>)null);

            var userControllerTest = SetUpUsersController(mockUserManager);

            // Called GetUsers("") to detect the Authorize attr
            // Need to differentiate between overloads
            var actualAttribute = userControllerTest
                .GetType()
                .GetMethod("GetUsers", new Type[] { typeof(string) })
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute[0].GetType());
        }

        [Fact(DisplayName = "UsersController: Register has AllowAnonymousAttribute")]
        public void RegisterHasUnAuthorizedAttribute()
        {
            var mockUserManager = new Mock<IUserManager>();
            mockUserManager
                .Setup(x => x.Register(new User(), ""))
                .Returns(Task.FromResult(new User()));

            var userControllerTest = SetUpUsersController(mockUserManager);

            var actualAttribute = userControllerTest
                .GetType()
                .GetMethod("Register")
                .GetCustomAttributes(typeof(AllowAnonymousAttribute), true);

            Assert.Equal(typeof(AllowAnonymousAttribute), actualAttribute[0].GetType());
        }

        [Fact(DisplayName = "UsersController: Authenticate BadRequest Missing user or pass")]
        public async Task AuthenticateUserReturnsBadRequestForMissingUsernameOrPassword()
        {
            // Arrange
            var moqUser = TestHelpers.Entities.GetTestUser();

            var mockUserManager = new Mock<IUserManager>();
            mockUserManager
                .Setup(x => x.Authenticate(moqUser.Email, "N00sh1970"))
                .Returns(Task.FromResult(moqUser));

            var userControllerTest = SetUpUsersController(mockUserManager);

            //var bodyString = @"{email: ""e@bubbanoosh.com.au"", password: ""N00sh1970""}";
            // var response = await _client.PostAsync("/api/users/login", new StringContent(bodyString, Encoding.UTF8, "application/json"));

            const string missingPassword = "";
            var userDto = new UserDto() { Email = "e@bubbanoosh.com.au", Password = missingPassword };

            var result = await userControllerTest.Authenticate(userDto);
            Assert.IsType<BadRequestObjectResult>(result);

            var responseString = result.As<ObjectResult>();
            Assert.Contains("Username or password is incorrect", responseString.Value.ToString());            
        }

        [Fact(DisplayName = "UsersController: Authenticate and get Token")]
        public async Task AuthenticateUserReturnsOkWithToken()
        {
            // Arrange
            var moqUser = TestHelpers.Entities.GetTestUser();
            const string password = "N00sh1970";

            var mockUserManager = new Mock<IUserManager>();
            mockUserManager
                .Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(moqUser);

            var userControllerTest = SetUpUsersController(mockUserManager);

            //var bodyString = @"{email: ""e@bubbanoosh.com.au"", password: ""N00sh1970""}";
            // var response = await _client.PostAsync("/api/users/login", new StringContent(bodyString, Encoding.UTF8, "application/json"));

            var userDto = new UserDto() { Email = "e@bubbanoosh.com.au", Password = password };

            var result = await userControllerTest.Authenticate(userDto);
            var okResult = Assert.IsType<OkObjectResult>(result);

            var responseString = result.As<string>();
            var responseJson = JObject.Parse(responseString);
            Assert.NotNull((string)responseJson["token"]);
        }

        //[Fact(DisplayName = "UsersController: GetUsers with Authorisation")]
        //public async Task GetUsersList()
        //{
        //    var bodyString = @"{username: ""e@bubbanoosh.com.au"", password: ""N00sh1970""}";
        //    var response = await _client.PostAsync("/api/users", new StringContent(bodyString, Encoding.UTF8, "application/json"));
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    var responseJson = JObject.Parse(responseString);
        //    var token = (string)responseJson["token"];

        //    var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/users");
        //    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var usersResponse = await _client.SendAsync(requestMessage);

        //    Assert.Equal(HttpStatusCode.OK, usersResponse.StatusCode);

        //    var userResponseString = await usersResponse.Content.ReadAsStringAsync();
        //    var userResponseJson = JArray.Parse(userResponseString);
        //    Assert.True(userResponseJson.Count == 4);
        //}
    }
}
