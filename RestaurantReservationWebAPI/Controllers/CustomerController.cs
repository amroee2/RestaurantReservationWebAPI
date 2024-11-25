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
    }
}
