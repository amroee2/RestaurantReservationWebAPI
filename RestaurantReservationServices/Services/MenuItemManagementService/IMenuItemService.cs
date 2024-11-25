using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationServices.Services.MenuItemManagementService
{
    public interface IMenuItemService
    {
        Task GetAllMenuItemsAsync();
        Task GetMenuItemByIdAsync(int id);
        Task AddMenuItemAsync(MenuItem menuItem);
        Task UpdateMenuItemAsync(int menuItemId, MenuItem menuItem);
        Task DeleteMenuItemAsync(int menuItemId);
        Task GetMenuItemsByReservationIdAsync(int restaurantId);
    }
}
