using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationServices.DTOs.RestaurantDTOs;
using RestaurantReservationServices.Exceptions;
using RestaurantReservationServices.Services.RestaurantManagementService;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/Restaurants")]
    [Authorize]
    [ApiController]
    public class RestaurantController : ControllerBase
    {

        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;
        public RestaurantController(IRestaurantService restaurantService, IMapper mapper)
        {
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<RestaurantReadDTO>>> GetAllRestaurants()
        {
            return Ok(await _restaurantService.GetAllRestaurantsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantReadDTO>> GetRestaurantById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Restaurant Id must be larger than 0");
            }
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
                return Ok(restaurant);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(RestaurantCreateDTO restaurantDto)
        {
            int restaurantID = await _restaurantService.AddRestaurantAsync(restaurantDto);

            var response = new
            {
                restaurantId = restaurantID,
                restaurant = restaurantDto
            };

            return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurantID }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, RestaurantUpdateDTO restaurantDto)
        {
            if (id <= 0)
            {
                return BadRequest("Restaurant Id must be larger than 0");
            }
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _restaurantService.UpdateRestaurantAsync(id, restaurantDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Restaurant Id must be larger than 0");
            }
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _restaurantService.DeleteRestaurantAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateRestaurant(int id, JsonPatchDocument<RestaurantUpdateDTO> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest("Restaurant Id must be larger than 0");
            }
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
                var restaurantToPatch = _mapper.Map<RestaurantUpdateDTO>(restaurant);
                patchDoc.ApplyTo(restaurantToPatch, ModelState);
                if (!TryValidateModel(restaurantToPatch))
                {
                    return ValidationProblem(ModelState);
                }
                await _restaurantService.UpdateRestaurantAsync(id, restaurantToPatch);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/revenue")]
        public async Task<ActionResult<decimal>> CalculateRestaurantRevenue(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Restaurant Id must be larger than 0");
            }
            try
            {
                var revenue = await _restaurantService.CalculateRestaurantRevenueAsync(id);
                return Ok(revenue);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
