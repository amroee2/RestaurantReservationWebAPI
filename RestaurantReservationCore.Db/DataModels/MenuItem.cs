namespace RestaurantReservationCore.Db.DataModels
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public required string Name { get; set; }
        public required double Price { get; set; }
        public required string Description { get; set; }
        public Restaurant Restaurant { get; set; }
        public required int RestaurantId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public MenuItem()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
