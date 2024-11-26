namespace RestaurantReservationServices.DTOs.OrderItemDTOs
{
    public class OrderItemUpdateDTO
    {
        public int Quantity { get; set; }
        public int MenuItemId { get; set; }
        public int OrderId { get; set; }
    }
}
