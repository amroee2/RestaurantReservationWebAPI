using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationServices.DTOs.OrderItemDTOs;
using RestaurantReservationServices.Exceptions;
using RestaurantReservationServices.Services.MenuItemManagementService;
using RestaurantReservationServices.Services.OrderItemManagementService;
using RestaurantReservationServices.Services.OrderManagementService;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/OrderItems")]
    [Authorize]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IOrderService _orderService;
        private readonly IMenuItemService _menuItemService;
        private readonly IMapper _mapper;

        public OrderItemController(IOrderService orderService, IOrderItemService orderItemService,
            IMenuItemService menuItemService, IMapper mapper)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _menuItemService = menuItemService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderItems()
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        [HttpGet("{id}", Name = "GetOrderItemById")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Id must be larger than 0");
            }
            try
            {
                var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);

                return Ok(orderItem);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderItem(OrderItemCreateDTO orderItemDto)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderItemDto.OrderId);
                var menuItem = await _menuItemService.GetMenuItemByIdAsync(orderItemDto.MenuItemId);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            int orderItemId = await _orderItemService.AddOrderItemAsync(orderItemDto);
            var response = new
            {
                OrderItemId = orderItemId,
                OrderItem = orderItemDto
            };

            return CreatedAtRoute("GetOrderItemById", new { id = orderItemId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItemUpdateDTO orderItemDto)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be larger than 0");
            }

            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderItemDto.OrderId);
                var menuItem = await _menuItemService.GetMenuItemByIdAsync(orderItemDto.MenuItemId);
                var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _orderItemService.UpdateOrderItemAsync(id, orderItemDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be larger than 0");
            }
            try
            {
                var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _orderItemService.DeleteOrderItemAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOrderItem(int id, JsonPatchDocument<OrderItemUpdateDTO> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be larger than 0");
            }
            try
            {
                var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
                var orderItemToPatch = _mapper.Map<OrderItemUpdateDTO>(orderItem);
                patchDoc.ApplyTo(orderItemToPatch, ModelState);

                if (!TryValidateModel(orderItemToPatch))
                {
                    return ValidationProblem(ModelState);
                }
                var order = await _orderService.GetOrderByIdAsync(orderItemToPatch.OrderId);
                var menuItem = await _menuItemService.GetMenuItemByIdAsync(orderItemToPatch.MenuItemId);
                await _orderItemService.UpdateOrderItemAsync(id, orderItemToPatch);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
