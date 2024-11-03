using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories.MenuItemManagement
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        Task<List<MenuItem>> GetMenuItemsByReservationIdAsync(int reservationId);
    }
}