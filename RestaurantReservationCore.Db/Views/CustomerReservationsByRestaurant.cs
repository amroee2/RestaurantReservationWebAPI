using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservationCore.Db.Views
{
    public class CustomerReservationsByRestaurant
    {
        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }

        [Column("Name")]
        public string RestaurantName { get; set; }

        public override string ToString()
        {
            return $"{CustomerName}, {ReservationDate}, {RestaurantName}";
        }
    }
}
