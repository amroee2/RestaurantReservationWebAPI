using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<decimal> CalculateRestaurantRevenueAsync(int restaurantId);
    }
}
