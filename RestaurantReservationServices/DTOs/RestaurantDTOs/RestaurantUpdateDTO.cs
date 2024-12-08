namespace RestaurantReservationServices.DTOs.RestaurantDTOs
{
    public class RestaurantUpdateDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? OpeningHours { get; set; }
    }
}
