using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Views;

namespace RestaurantReservationCore.Db.Repositories.ReservationManagement
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<List<Reservation>> GetReservationsByCustomerIdAsync(int customerId);

        Task<List<CustomerReservationsByRestaurant>> GetCustomerReservationsByRestaurants();
    }
}