namespace RestaurantReservationCore.Db.DataModels
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public MenuItem MenuItem { get; set; }
        public int MenuItemId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }

        public override string ToString()
        {
            return $"Order Item Id: {OrderItemId}, Quantity: {Quantity}, Menu Item Id: {MenuItemId}, Order Id: {OrderId}\nMenuItem: {MenuItem}";
        }
    }
}
