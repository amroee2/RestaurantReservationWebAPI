namespace RestaurantReservationServices.DTOs.TableDTOs
{
    public class TableReadDTO
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }
        public int RestaurantId { get; set; }
    }
}
