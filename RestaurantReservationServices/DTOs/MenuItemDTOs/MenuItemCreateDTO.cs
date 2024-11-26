namespace RestaurantReservationServices.DTOs.MenuItemDTOs
{
    public class MenuItemCreateDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int RestaurantId { get; set; }
    }
}
