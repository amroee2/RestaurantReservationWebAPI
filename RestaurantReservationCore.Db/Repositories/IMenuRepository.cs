using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories
{
    public interface IMenuRepository : IRepository<MenuItem>
    {
        Task<List<MenuItem>> GetMenuItemsByReservationIdAsync(int reservationId);
    }
}
