using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.ReservationDTOs;

namespace RestaurantReservationServices.Services.ReservationManagementService
{
    public interface IReservationService
    {
        Task<List<ReservationReadDTO>> GetAllReservationsAsync();
        Task<ReservationReadDTO> GetReservationByIdAsync(int id);
        Task<int> AddReservationAsync(ReservationCreateDTO reservation);
        Task UpdateReservationAsync(int id, ReservationUpdateDTO reservation);
        Task DeleteReservationAsync(int id);
    }
}
