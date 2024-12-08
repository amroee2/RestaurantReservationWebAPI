using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.RestaurantDTOs;

namespace RestaurantReservationServices.Services.RestaurantManagementService
{
    public interface IRestaurantService
    {
        Task<List<RestaurantReadDTO>> GetAllRestaurantsAsync();
        Task<RestaurantReadDTO> GetRestaurantByIdAsync(int id);
        Task<int> AddRestaurantAsync(RestaurantCreateDTO restaurant);
        Task UpdateRestaurantAsync(int id, RestaurantUpdateDTO restaurant);
        Task DeleteRestaurantAsync(int id);
        Task <decimal> CalculateRestaurantRevenueAsync(int restaurantId);
    }
}
