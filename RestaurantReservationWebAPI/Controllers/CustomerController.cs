using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationServices.DTOs.CustomerDTOs;
using RestaurantReservationServices.Services.CustomerManagementService;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCustomers()
        {
            return Ok(await _customerService.GetAllCustomersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerById(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Customer Id must be larger than 0");
            }
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerCreateDTO customerDto)
        {
            int customerID = await _customerService.AddCustomerAsync(customerDto);

            var response = new
            {
                customerId = customerID,
                customer = customerDto
            };

            return CreatedAtAction(nameof(GetCustomerById), new { id = customerID }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Customer Id must be larger than 0");
            }
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound("Customer doesn't exit");
            }
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdateDTO customerDto)
        {
            if (id <= 0)
            {
                return BadRequest("Customer Id must be larger than 0");
            }
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound("Customer doesn't exit");
            }
            await _customerService.UpdateCustomerAsync(id, customerDto);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateCustomer(int id, JsonPatchDocument<CustomerUpdateDTO> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest("Customer Id must be larger than 0");
            }
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound("Customer doesn't exit");
            }
            var customerToPatch = new CustomerUpdateDTO()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
            patchDoc.ApplyTo(customerToPatch, ModelState);
            if (!TryValidateModel(customerToPatch))
            {
                return ValidationProblem(ModelState);
            }
            await _customerService.UpdateCustomerAsync(id, customerToPatch);
            return NoContent();
        }

        [HttpGet("bigPartySize/{partySize}")]
        public async Task<ActionResult> GetCustomersWithBigPartySize(int partySize)
        {
            if (partySize <= 0)
            {
                return BadRequest("Party size must be larger than 0");
            }
            return Ok(await _customerService.GetCustomersWithBigPartySizeAsync(partySize));
        }
    }
}
