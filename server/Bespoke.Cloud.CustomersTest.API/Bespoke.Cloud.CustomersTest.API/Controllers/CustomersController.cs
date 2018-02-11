using Bespoke.Cloud.CustomersTest.API.Helpers;
using Bespoke.Cloud.CustomersTest.Business.Interfaces;
using Bespoke.Cloud.CustomersTest.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Bespoke.Cloud.CustomersTest.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private ILogger<CustomersController> _logger;
        private ICustomerManager _customerManager;

        public CustomersController(ICustomerManager customerManger,
            ILogger<CustomersController> logger)
        {
            _customerManager = customerManger;
            _logger = logger;
        }

        // GET: api/Customers
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerManager.GetCustomers("");

            return Ok(customers);
        }

        // GET: api/Customers/list/searchText?
        [HttpGet("list/{searchText?}", Name = "GetSearch")]
        public IActionResult GetCustomers(string searchText = "")
        {
            var customers = _customerManager.GetCustomers(searchText);

            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var customer = _customerManager.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound($"No Customer found with id: {id}");
            }
            else
            {
                return Ok(customer);
            }
        }

        // POST: api/Customers
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            var id = _customerManager.AddCustomer(customer);
            if (id <= 0)
            {
                ModelState.AddModelError(nameof(Customer),
                    "Oops, Customer already exists!");

                // return 422 - Unprocessable Entity
                return new UnprocessableEntityObjectResult(ModelState);
            }
            else
            {
                customer.Id = id;

                return CreatedAtRoute("Get",
                    new { id },
                    customer);
                //customer.Id = id;
                //return Ok(customer);
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            var customerToUpdate = _customerManager.GetCustomerById(customer.Id);

            if (customerToUpdate == null)
            {
                return NotFound($"No Customer found to 'Update' with id: {id}");
            }
            else
            {
                var updated = _customerManager.UpdateCustomer(customer);
                if (!updated)
                {
                    throw new Exception($"Customer {id} could not be updated on save.");
                }

                return NoContent(); // 204 No Content
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _customerManager.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound($"No Customer found to 'Delete' with id: {id}");
            }
            else
            {
                if (!_customerManager.RemoveCustomer(id))
                {
                    throw new Exception($"Deleting customer {id} failed on save.");
                }

                _logger.LogInformation(100, $"Customer {id} was DELETED!!");

                return NoContent(); // 204 No Content
            }
        }
    }
}
