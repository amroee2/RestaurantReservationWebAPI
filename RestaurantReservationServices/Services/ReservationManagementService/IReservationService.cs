using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationServices.Services.ReservationManagementService
{
    public interface IReservationService
    {
        Task GetAllReservationsAsync();
        Task GetReservationByIdAsync(int id);
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(int id, Reservation reservation);
    }
}
