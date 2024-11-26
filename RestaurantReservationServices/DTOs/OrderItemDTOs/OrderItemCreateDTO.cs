using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationServices.DTOs.OrderItemDTOs
{
    public class OrderItemCreateDTO
    {
        public int Quantity { get; set; }
        public int MenuItemId { get; set; }
        public int OrderId { get; set; }
    }
}
