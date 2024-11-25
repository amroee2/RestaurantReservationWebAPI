using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationServices.Services.OrderManagementService
{
    public interface IOrderService
    {
        Task GetAllOrdersAsync();
        Task GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(int id, Order order);
        Task DeleteOrderAsync(int id);
        Task GetOrdersByReservationIdAsync(int reservationId);
    }
}
