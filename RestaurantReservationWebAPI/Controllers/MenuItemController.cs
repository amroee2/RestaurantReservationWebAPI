using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationServices.DTOs.MenuItemDTOs;
using RestaurantReservationServices.Exceptions;
using RestaurantReservationServices.Services.MenuItemManagementService;
using RestaurantReservationServices.Services.RestaurantManagementService;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/MenuItems")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IMapper _mapper;
        private readonly IRestaurantService _restaurantService;
        public MenuItemController(IMenuItemService menuItemService, IMapper mapper, IRestaurantService restaurantService)
        {
            _menuItemService = menuItemService;
            _mapper = mapper;
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuItemsAsync()
        {
            return Ok(await _menuItemService.GetAllMenuItemsAsync());
        }

        [HttpGet("{id}", Name ="GetMenuItemById")]
        public async Task<IActionResult> GetMenuItemByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Menu Item Id must be larger than 0");
            }
            try
            {
                var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
                return Ok(menuItem);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuItemAsync(MenuItemCreateDTO menuItemDto)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(menuItemDto.RestaurantId);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            int menuItemId = await _menuItemService.AddMenuItemAsync(menuItemDto);

            var response = new
            {
                menuItemId = menuItemId,
                menuItem = menuItemDto
            };

            return CreatedAtAction("GetMenuItemById", new { id = menuItemId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItemAsync(int id, MenuItemUpdateDTO menuItemDto)
        {
            if (id <= 0)
            {
                return BadRequest("Menu Item Id must be larger than 0");
            }
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(menuItemDto.RestaurantId);
                var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _menuItemService.UpdateMenuItemAsync(id, menuItemDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItemAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Menu Item Id must be larger than 0");
            }
            try
            {
                var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _menuItemService.DeleteMenuItemAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateMenuItem(int id, JsonPatchDocument<MenuItemUpdateDTO> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest("Menu Item Id must be larger than 0");
            }
            try
            {
                var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
                var menuItemToPatch = _mapper.Map<MenuItemUpdateDTO>(menuItem);
                patchDoc.ApplyTo(menuItemToPatch, ModelState);
                if (!TryValidateModel(menuItemToPatch))
                {
                    return ValidationProblem(ModelState);
                }
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(menuItemToPatch.RestaurantId);
                await _menuItemService.UpdateMenuItemAsync(id, menuItemToPatch);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
