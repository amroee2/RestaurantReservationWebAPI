using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories.RestaurantManagement
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<decimal> CalculateRestaurantRevenueAsync(int restaurantId);
    }
}