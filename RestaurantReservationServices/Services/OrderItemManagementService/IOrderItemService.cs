using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.OrderItemDTOs;

namespace RestaurantReservationServices.Services.OrderItemManagementService
{
    public interface IOrderItemService
    {
        Task<List<OrderItemReadDTO>> GetAllOrderItemsAsync();
        Task<OrderItemReadDTO> GetOrderItemByIdAsync(int id);
        Task<int> AddOrderItemAsync(OrderItemCreateDTO orderItem);
        Task UpdateOrderItemAsync(int id, OrderItemUpdateDTO orderItem);
        Task DeleteOrderItemAsync(int id);
    }
}
