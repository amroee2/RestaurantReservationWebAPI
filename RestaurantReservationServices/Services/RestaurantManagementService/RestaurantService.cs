using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.RestaurantManagement;
using RestaurantReservationServices.DTOs.RestaurantDTOs;
using RestaurantReservationServices.Exceptions;

namespace RestaurantReservationServices.Services.RestaurantManagementService
{
    public class RestaurantService: IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task<List<RestaurantReadDTO>> GetAllRestaurantsAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return _mapper.Map<List<RestaurantReadDTO>>(restaurants);
        }

        public async Task<RestaurantReadDTO> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                throw new EntityNotFoundException("Restaurant not found");
            }
            return _mapper.Map<RestaurantReadDTO>(restaurant);
        }

        public async Task<int> AddRestaurantAsync(RestaurantCreateDTO restaurant)
        {
            var newRestaurant = _mapper.Map<Restaurant>(restaurant);
            await _restaurantRepository.AddAsync(newRestaurant);
            return newRestaurant.RestaurantId;
        }

        public async Task UpdateRestaurantAsync(int Id, RestaurantUpdateDTO restaurant)
        {
            var restaurantToUpdate = await _restaurantRepository.GetByIdAsync(Id);
            if (restaurant == null)
            {
                throw new EntityNotFoundException("Restaurant not found");
            }
            _mapper.Map(restaurant, restaurantToUpdate);
            await _restaurantRepository.UpdateAsync(restaurantToUpdate);
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                throw new EntityNotFoundException("Restaurant not found");
            }
            await _restaurantRepository.DeleteAsync(restaurant);
        }

        public async Task<decimal> CalculateRestaurantRevenueAsync(int restaurantId)
        {
            var totalRevenue = await _restaurantRepository.CalculateRestaurantRevenueAsync(restaurantId);
            return totalRevenue;
        }
    }
}