namespace RestaurantReservationCore.Db.DataModels
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Position { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
        public List<Order> Orders { get; set; }

        public Employee()
        {
            Orders = new List<Order>();
        }

        public override string ToString()
        {
            return $"{EmployeeId}, {FirstName}, {LastName}, {Position}, {RestaurantId}";
        }
    }
}