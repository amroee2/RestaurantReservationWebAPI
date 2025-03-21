﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationServices.DTOs.MenuItemDTOs;
using RestaurantReservationServices.DTOs.OrderDTOs;
using RestaurantReservationServices.DTOs.ReservationDTOs;
using RestaurantReservationServices.Exceptions;
using RestaurantReservationServices.Services.CustomerManagementService;
using RestaurantReservationServices.Services.MenuItemManagementService;
using RestaurantReservationServices.Services.OrderManagementService;
using RestaurantReservationServices.Services.ReservationManagementService;
using RestaurantReservationServices.Services.RestaurantManagementService;
using RestaurantReservationServices.Services.TableManagementService;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/Reservations")]
    [Authorize]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly ITableService _tableService;
        private readonly IRestaurantService _restaurantService;
        private readonly IMenuItemService _menuItemService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService, ICustomerService customerService,
            ITableService tableService, IMapper mapper, IRestaurantService restaurantService, 
            IMenuItemService menuItemService, IOrderService orderService)
        {
            _reservationService = reservationService;
            _customerService = customerService;
            _tableService = tableService;
            _mapper = mapper;
            _restaurantService = restaurantService;
            _menuItemService = menuItemService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationReadDTO>>> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}", Name = "GetReservationById")]
        public async Task<ActionResult<ReservationReadDTO>> GetReservationById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Reservation Id must be larger than 0");
            }
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                return Ok(reservation);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReservationReadDTO>> AddReservation(ReservationCreateDTO reservationDto)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(reservationDto.CustomerId);
                var table = await _tableService.GetTableByIdAsync(reservationDto.TableId);
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(reservationDto.RestaurantId);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            var newReservationId = await _reservationService.AddReservationAsync(reservationDto);

            var response = new
            {
                reservationId = newReservationId,
                reservation = reservationDto
            };
            return CreatedAtRoute("GetReservationById", new { id = newReservationId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationUpdateDTO reservationDto)
        {
            if (id <= 0)
            {
                return BadRequest("Reservation Id must be larger than 0");
            }
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(reservationDto.CustomerId);
                var table = await _tableService.GetTableByIdAsync(reservationDto.TableId);
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(reservationDto.RestaurantId);
                var reservation = await _reservationService.GetReservationByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _reservationService.UpdateReservationAsync(id, reservationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Reservation Id must be larger than 0");
            }
            try
            {
                await _reservationService.GetReservationByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _reservationService.DeleteReservationAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateReservatiob(int id, JsonPatchDocument<ReservationUpdateDTO> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest("Reservation Id must be larger than 0");
            }
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                var reservationToPatch = _mapper.Map<ReservationUpdateDTO>(reservation);
                patchDoc.ApplyTo(reservationToPatch, ModelState);
                if (!TryValidateModel(reservationToPatch))
                {
                    return ValidationProblem(ModelState);
                }
                var customer = await _customerService.GetCustomerByIdAsync(reservationToPatch.CustomerId);
                var table = await _tableService.GetTableByIdAsync(reservationToPatch.TableId);
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(reservationToPatch.RestaurantId);
                await _reservationService.UpdateReservationAsync(id, reservationToPatch);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Customer/{customerId}")]
        public async Task<ActionResult<List<ReservationReadDTO>>> GetReservationsByCustomerId(int customerId)
        {
            if (customerId <= 0)
            {
                return BadRequest("Customer Id must be larger than 0");
            }
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(customerId);
                var reservations = await _reservationService.GetReservationsByCustomerIdAsync(customerId);
                return Ok(reservations);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/menu-items")]
        public async Task<ActionResult<List<MenuItemReadDTO>>> GetMenuItemsByReservationId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Reservation Id must be larger than 0");
            }
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                var menuItems = await _menuItemService.GetMenuItemsByReservationIdAsync(id);
                return Ok(menuItems);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/orders")]
        public async Task<ActionResult<List<OrderReadDTO>>> GetOrdersByReservationId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Reservation Id must be larger than 0");
            }
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                var orders = await _orderService.GetOrdersByReservationIdAsync(id);
                return Ok(orders);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
