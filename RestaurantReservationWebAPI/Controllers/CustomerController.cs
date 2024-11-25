using Microsoft.AspNetCore.Mvc;
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
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
