using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories.OrderManagement
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersByReservationIdAsync(int reservationId);
    }
}