namespace RestaurantReservation.Db.DataModels
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Position { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
        public List<Order> Orders { get; set; }
        public Employee()
        {
            Orders = new List<Order>();
        }
    }
}
