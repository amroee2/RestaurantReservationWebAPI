namespace RestaurantReservationServices.DTOs.OrderDTOs
{
    public class OrderCreateDTO
    {
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public int EmployeeId { get; set; }
        public int ReservationId { get; set; }
    }
}
