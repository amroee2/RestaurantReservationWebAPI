using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationServices.DTOs.EmployeeDTOs;
using RestaurantReservationServices.Exceptions;
using RestaurantReservationServices.Services.EmployeeManagementService;
using RestaurantReservationServices.Services.RestaurantManagementService;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IRestaurantService _restaurantService;
        public EmployeeController(IEmployeeService employeeService, IMapper mapper, IRestaurantService restaurantService)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeReadDTO>>> GetAllEmployeesAsync()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}", Name ="GetEmployeeById")]
        public async Task<ActionResult<EmployeeReadDTO>> GetEmployeeByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Employee Id must be larger than 0");
            }
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                return Ok(employee);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync(EmployeeCreateDTO employeeDto)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(employeeDto.RestaurantId);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            int employeeId = await _employeeService.AddEmployeeAsync(employeeDto);

            var response = new
            {
                EmployeeId = employeeId,
                employee = employeeDto
            };

            return CreatedAtAction("GetEmployeeById", new { id = employeeId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, EmployeeUpdateDTO employeeDto)
        {
            if (id <= 0)
            {
                return BadRequest("Employee Id must be larger than 0");
            }
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(employeeDto.RestaurantId);
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _employeeService.UpdateEmployeeAsync(id, employeeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Employee Id must be larger than 0");
            }
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateEmployee(int id, JsonPatchDocument<EmployeeUpdateDTO> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest("Employee Id must be larger than 0");
            }
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                var employeeToPatch = _mapper.Map<EmployeeUpdateDTO>(employee);
                patchDoc.ApplyTo(employeeToPatch, ModelState);
                if (!TryValidateModel(employeeToPatch))
                {
                    return ValidationProblem(ModelState);
                }
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(employeeToPatch.RestaurantId);
                await _employeeService.UpdateEmployeeAsync(id, employeeToPatch);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Managers")]
        public async Task<ActionResult<List<EmployeeReadDTO>>> GetAllManagersAsync()
        {
            var managers = await _employeeService.GetAllManagersAsync();
            return Ok(managers);
        }

        [HttpGet("{employeeId}/average-order-amount")]
        public async Task<ActionResult<double>> CalculateAverageOrderAmountAsync(int employeeId)
        {
            if (employeeId <= 0)
            {
                return BadRequest("Employee Id must be larger than 0");
            }
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
                var averageOrderAmount = await _employeeService.CalculateAverageOrderAmountAsync(employeeId);
                return Ok(averageOrderAmount);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
