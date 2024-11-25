using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationServices.DTOs.CustomerDTOs;
using RestaurantReservationServices.DTOs.RestaurantDTOs;
using RestaurantReservationServices.Services.RestaurantManagementService;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/Restaurants")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {

        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
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
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
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
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
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
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
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
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound("Customer doesn't exit");
            }
            var restaurantToPatch = new RestaurantUpdateDTO()
            {
                Name = restaurant.Name,
                Address = restaurant.Address,
                PhoneNumber = restaurant.PhoneNumber,
                OpeningHours = restaurant.OpeningHours
            };
            patchDoc.ApplyTo(restaurantToPatch, ModelState);
            if (!TryValidateModel(restaurantToPatch))
            {
                return ValidationProblem(ModelState);
            }
            await _restaurantService.UpdateRestaurantAsync(id, restaurantToPatch);
            return NoContent();
        }
    }
}
