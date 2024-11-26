using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.MenuItemDTOs;

namespace RestaurantReservationServices.Services.MenuItemManagementService
{
    public interface IMenuItemService
    {
        Task<List<MenuItemReadDTO>> GetAllMenuItemsAsync();
        Task<MenuItemReadDTO> GetMenuItemByIdAsync(int id);
        Task<int> AddMenuItemAsync(MenuItemCreateDTO menuItem);
        Task UpdateMenuItemAsync(int menuItemId, MenuItemUpdateDTO menuItem);
        Task DeleteMenuItemAsync(int menuItemId);
        Task<List<MenuItemReadDTO>> GetMenuItemsByReservationIdAsync(int restaurantId);
    }
}
