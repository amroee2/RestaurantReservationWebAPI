using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationServices.Services.OrderItemManagementService
{
    public interface IOrderItemService
    {
        Task GetAllOrderItemsAsync();
        Task GetOrderItemByIdAsync(int id);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(int id, OrderItem orderItem);
    }
}
