namespace RestaurantReservationServices.DTOs.OrderDTOs
{
    public class OrderReadDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public int EmployeeId { get; set; }
        public int ReservationId { get; set; }
    }
}
