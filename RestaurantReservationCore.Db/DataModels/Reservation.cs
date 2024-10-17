namespace RestaurantReservationCore.Db.DataModels
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int PartySize { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Table Table { get; set; }
        public int TableId { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
        public List<Order> Orders { get; set; }
        public Reservation()
        {
            Orders = new List<Order>();
        }

        public override string ToString()
        {
            return $"Reservation Id: {ReservationId}, Reservation Date: {ReservationDate}, Party Size: {PartySize}, CustomerId: {CustomerId}, TableId: {TableId}, RestaurantId: {RestaurantId}";
        }
    }
}
