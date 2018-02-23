using Bespoke.Cloud.CustomersTest.Entities;
using Moq;
using Xunit;
using System;
using Bespoke.Cloud.CustomersTest.Repository.Interfaces;
using Bespoke.Cloud.CustomersTest.Business.Interfaces;
using Bespoke.Cloud.CustomersTest.API.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Bespoke.Cloud.CustomersTest.API.Helpers;
using Bespoke.Cloud.CustomersTest.API.Models;
using System.Collections.Generic;
using AutoMapper;
using Bespoke.Cloud.CustomersTest.Repository.Tests.EqualityComparers;

namespace Bespoke.Cloud.CustomersTest.Repository.Tests
{
    public class CustomersControllerTests
    {
        [Fact(DisplayName = "CustomersController: GetCustomersReturnsCustomers")]
        public void GetCustomersReturnsCustomers()
        {
            // Arrange
            IList<Customer> moqCustomersList = TestHelpers.Entities.GetTestCustomersList();
            IList<CustomerListDto> moqCustomerListDto = TestHelpers.Entities.GetTestCustomersListDto();

            // -- AutoMapper Config for TEST
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Customer, API.Models.CustomerListDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                        $"{src.FirstName} {src.LastName}"));

            });
            Mapper.AssertConfigurationIsValid();

            var mockCustomerManager = new Mock<ICustomerManager>();
            mockCustomerManager.Setup(x => x.GetCustomers("")).Returns(moqCustomersList);
            var mockILogger = new Mock<ILogger<CustomersController>>();

            var customerControllerTest = new CustomersController(mockCustomerManager.Object, mockILogger.Object);

            // Act
            IActionResult result = customerControllerTest.GetCustomers(string.Empty);
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            var customersEqualityComparer = new CustomersEqualityComparer();
            Assert.Equal(moqCustomerListDto, (IList<CustomerListDto>)okResult.Value, customersEqualityComparer);
        }

        [Fact(DisplayName = "CustomersController: GetCustomerById Returns A Customer")]
        public void GetCustomerByIdReturnsACustomer()
        {
            // Arrange
            Customer moqCustomer = TestHelpers.Entities.GetTestCustomer();
            var customerId = 1;

            var mockCustomerManager = new Mock<ICustomerManager>();
            mockCustomerManager.Setup(x => x.GetCustomerById(customerId)).Returns(moqCustomer);
            var mockILogger = new Mock<ILogger<CustomersController>>();

            var customerControllerTest = new CustomersController(mockCustomerManager.Object, mockILogger.Object);
            moqCustomer.Id = customerId;

            // Act
            IActionResult result = customerControllerTest.Get(customerId);
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.Equal(moqCustomer, okResult.Value);
        }

        [Fact(DisplayName = "CustomersController: GetCustomerById Returns NotFound")]
        public void GetCustomerByIdReturnsNotFound()
        {
            // Arrange
            var customerId = 1;

            var mockCustomerManager = new Mock<ICustomerManager>();
            mockCustomerManager.Setup(x => x.GetCustomerById(customerId)).Returns((Customer)null);
            var mockILogger = new Mock<ILogger<CustomersController>>();

            var customerControllerTest = new CustomersController(mockCustomerManager.Object, mockILogger.Object);

            // Act
            IActionResult result = customerControllerTest.Get(customerId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact(DisplayName = "CustomersController: Post Fails With BadRequest receiving a null Customer")]
        public void PostFailsWithBadRequest()
        {
            // Arrange
            Customer moqCustomer  = null;

            var mockCustomerManager = new Mock<ICustomerManager>();
            mockCustomerManager.Setup(x => x.AddCustomer(moqCustomer)).Returns(null);
            var mockILogger = new Mock<ILogger<CustomersController>>();

            var customerControllerTest = new CustomersController(mockCustomerManager.Object, mockILogger.Object);

            // Act
            IActionResult result = customerControllerTest.Post(moqCustomer);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact(DisplayName = "CustomersController: Post Fails With UnprocessableEntityObjectResult When Customer Exists (409)")]
        public void PostFailsWithUnprocessableEntityObjectResultWhenCustomerExists()
        {
            // Arrange
            Customer moqCustomer = TestHelpers.Entities.GetTestCustomer();
            var customerIdReturnZero = 0;

            var mockCustomerManager = new Mock<ICustomerManager>();
            mockCustomerManager.Setup(x => x.AddCustomer(moqCustomer)).Returns(customerIdReturnZero);
            var mockILogger = new Mock<ILogger<CustomersController>>();

            var customerControllerTest = new CustomersController(mockCustomerManager.Object, mockILogger.Object);

            customerControllerTest.ModelState.AddModelError(nameof(Customer),
                    "Oops, Customer already exists!");

            // Act
            IActionResult result = customerControllerTest.Post(moqCustomer);
            var unprocessableEntityResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            var returnResult = new SerializableError(customerControllerTest.ModelState);

            // Assert
            Assert.Equal(returnResult, unprocessableEntityResult.Value);
        }

        [Fact(DisplayName = "CustomersController: Post Succeeds With CreatedAtRoute ")]
        public void PostSucceedsWithCreatedAtRoute()
        {
            // Arrange
            Customer moqCustomer = TestHelpers.Entities.GetTestCustomer();

            var newCustomerId = 1;

            var mockCustomerManager = new Mock<ICustomerManager>();
            mockCustomerManager.Setup(x => x.AddCustomer(moqCustomer)).Returns(newCustomerId);
            var mockILogger = new Mock<ILogger<CustomersController>>();

            var customerControllerTest = new CustomersController(mockCustomerManager.Object, mockILogger.Object);
            moqCustomer.Id = newCustomerId;

            // Act
            IActionResult result = customerControllerTest.Post(moqCustomer);
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);

            // Assert
            Assert.Equal(moqCustomer, createdAtRouteResult.Value);
        }


    }
}
