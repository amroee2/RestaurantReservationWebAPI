namespace RestaurantReservationCore.Db.DataModels
{
    public class Table
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<Reservation> Reservations { get; set; }

        public Table()
        {
            Reservations = new List<Reservation>();
        }

        public override string ToString()
        {
            return $"Table Id: {TableId}, Capacity: {Capacity}, RestaurantId: {RestaurantId}";
        }
    }
}