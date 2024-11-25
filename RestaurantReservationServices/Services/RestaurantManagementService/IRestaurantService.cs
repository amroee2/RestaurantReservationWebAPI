using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationServices.Services.RestaurantManagementService
{
    public interface IRestaurantService
    {
        Task GetAllRestaurantsAsync();
        Task GetRestaurantByIdAsync(int id);
        Task AddRestaurantAsync(Restaurant restaurant);
        Task UpdateRestaurantAsync(int id, Restaurant restaurant);
        Task CalculateRestaurantRevenueAsync(int restaurantId);
    }
}
