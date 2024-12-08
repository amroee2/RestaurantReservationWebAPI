using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.OrderDTOs;

namespace RestaurantReservationServices.Services.OrderManagementService
{
    public interface IOrderService
    {
        Task<List<OrderReadDTO>> GetAllOrdersAsync();
        Task<OrderReadDTO> GetOrderByIdAsync(int id);
        Task<int> AddOrderAsync(OrderCreateDTO order);
        Task UpdateOrderAsync(int id, OrderUpdateDTO order);
        Task DeleteOrderAsync(int id);
        Task<List<OrderReadDTO>> GetOrdersByReservationIdAsync(int reservationId);
    }
}
