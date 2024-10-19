using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;

namespace RestaurantReservationCore.Services
{
    public class MenuItemService
    {
        private readonly IMenuRepository _menuItemRepository;

        public MenuItemService(IMenuRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task GetAllMenuItemsAsync()
        {
            List<MenuItem> menuItems = await _menuItemRepository.GetAllAsync();
            if (!menuItems.Any())
            {
                Console.WriteLine("No menu items found");
            }
            foreach (var menuItem in menuItems)
            {
                Console.WriteLine(menuItem);
            }
        }

        public async Task GetMenuItemByIdAsync(int id)
        {
            MenuItem menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
            {
                Console.WriteLine("Menu item not found");
                return;
            }
            Console.WriteLine(menuItem);
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            MenuItem requestedMenuItem = await _menuItemRepository.GetByIdAsync(menuItem.MenuItemId);
            if (requestedMenuItem != null)
            {
                Console.WriteLine("Menu item already exists");
                return;
            }
            await _menuItemRepository.AddAsync(menuItem);
        }

        public async Task UpdateMenuItemAsync(int id, MenuItem menuItem)
        {
            MenuItem updatedItem = await _menuItemRepository.GetByIdAsync(id);
            if (updatedItem == null)
            {
                Console.WriteLine("Menu Item not found");
                return;
            }
            updatedItem.Name = menuItem.Name;
            updatedItem.Price = menuItem.Price;
            updatedItem.Description = menuItem.Description;
            updatedItem.RestaurantId = menuItem.RestaurantId;
            await _menuItemRepository.UpdateAsync(updatedItem);
        }

        public async Task DeleteMenuItemAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
            {
                Console.WriteLine("Menu item doesn't exist");
                return;
            }
            await _menuItemRepository.DeleteAsync(menuItem);
        }

        public async Task GetMenuItemsByReservationIdAsync(int reservationId)
        {
            var menuItems = await _menuItemRepository.GetMenuItemsByReservationIdAsync(reservationId);
            if (!menuItems.Any())
            {
                Console.WriteLine("No menu items found");
                return;
            }
            foreach (var menuItem in menuItems)
            {
                Console.WriteLine(menuItem);
            }
        }
    }
}