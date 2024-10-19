using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersByReservationIdAsync(int reservationId);
    }
}