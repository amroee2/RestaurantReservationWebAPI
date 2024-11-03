namespace RestaurantReservationCore.Db.DataModels
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? OpeningHours { get; set; }
        public List<Table> Tables { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        public Restaurant()
        {
            Tables = new List<Table>();
            Employees = new List<Employee>();
            Reservations = new List<Reservation>();
            MenuItems = new List<MenuItem>();
        }

        public override string ToString()
        {
            return $"{RestaurantId}, {Name}, {Address}, {PhoneNumber}, {OpeningHours}";
        }
    }
}