using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.CustomerDTOs;
using RestaurantReservationServices.DTOs.EmployeeDTOs;
using RestaurantReservationServices.Exceptions;
using RestaurantReservationServices.Services.CustomerManagementService;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCustomers()
        {
            return Ok(await _customerService.GetAllCustomersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Customer Id must be larger than 0");
            }
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                return Ok(customer);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
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
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
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
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
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
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                var customerToPatch = _mapper.Map<CustomerUpdateDTO>(customer);
                patchDoc.ApplyTo(customerToPatch, ModelState);
                if (!TryValidateModel(customerToPatch))
                {
                    return ValidationProblem(ModelState);
                }
                await _customerService.UpdateCustomerAsync(id, customerToPatch);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
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
