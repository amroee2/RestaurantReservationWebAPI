namespace RestaurantReservationServices.DTOs.EmployeeDTOs
{
    public class EmployeeReadDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int RestaurantId { get; set; }
    }
}
