namespace RestaurantReservationServices.DTOs.ReservationDTOs
{
    public class ReservationCreateDTO
    {
        public DateTime ReservationDate { get; set; }
        public int PartySize { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public int RestaurantId { get; set; }
    }
}
