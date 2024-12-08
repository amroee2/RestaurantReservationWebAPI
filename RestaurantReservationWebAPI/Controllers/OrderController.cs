using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationServices.DTOs.OrderDTOs;
using RestaurantReservationServices.Exceptions;
using RestaurantReservationServices.Services.EmployeeManagementService;
using RestaurantReservationServices.Services.OrderManagementService;
using RestaurantReservationServices.Services.ReservationManagementService;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/Orders")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IEmployeeService _employeeService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IEmployeeService employeeService,
            IReservationService reservationService, IMapper mapper)
        {
            _orderService = orderService;
            _employeeService = employeeService;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderReadDTO>>> GetAllOrdersAsync()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderReadDTO>> GetOrderByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id");
            }
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                return Ok(order);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderAsync(OrderCreateDTO orderDto)
        {
            try
            {
                await _employeeService.GetEmployeeByIdAsync(orderDto.EmployeeId);
                await _reservationService.GetReservationByIdAsync(orderDto.ReservationId);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            int orderId = await _orderService.AddOrderAsync(orderDto);
            var response = new
            {
                OrderId = orderId,
                Order = orderDto
            };
            return CreatedAtAction("GetOrderById", new { id = orderId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderAsync(int id, OrderUpdateDTO orderDto)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id");
            }
            try
            {
                var employee = _employeeService.GetEmployeeByIdAsync(orderDto.EmployeeId);
                var reservation = _reservationService.GetReservationByIdAsync(orderDto.ReservationId);
                await _orderService.GetOrderByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _orderService.UpdateOrderAsync(id, orderDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id");
            }
            try
            {
                await _orderService.GetOrderByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateEmployee(int id, JsonPatchDocument<OrderUpdateDTO> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id");
            }
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                var orderDto = _mapper.Map<OrderUpdateDTO>(order);
                patchDoc.ApplyTo(orderDto, ModelState);
                if (!TryValidateModel(orderDto))
                {
                    return ValidationProblem(ModelState);
                }
                var employee = await _employeeService.GetEmployeeByIdAsync(orderDto.EmployeeId);
                var reservation = await _reservationService.GetReservationByIdAsync(orderDto.ReservationId);
                await _orderService.UpdateOrderAsync(id, orderDto);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
