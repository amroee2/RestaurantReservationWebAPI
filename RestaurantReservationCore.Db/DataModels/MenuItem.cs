namespace RestaurantReservationCore.Db.DataModels
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public MenuItem()
        {
            OrderItems = new List<OrderItem>();
        }

        public override string ToString()
        {
            return $"Menu item Id: {MenuItemId},Name: {Name},Price: {Price},Description: {Description},Restaurant Id: {RestaurantId}";
        }
    }
}