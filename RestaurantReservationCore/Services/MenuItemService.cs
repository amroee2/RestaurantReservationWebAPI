using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;

namespace RestaurantReservationCore.Services
{
    public class MenuItemService
    {
        private readonly IRepository<MenuItem> _menuItemRepository;

        public MenuItemService(IRepository<MenuItem> menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<List<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _menuItemRepository.GetAllAsync();
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(int id)
        {
            return await _menuItemRepository.GetByIdAsync(id);
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            await _menuItemRepository.AddAsync(menuItem);
        }

        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            await _menuItemRepository.UpdateAsync(menuItem);
        }

        public async Task DeleteMenuItemAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
            {
                Console.WriteLine("Menu item doesn't exist");
            }
            await _menuItemRepository.DeleteAsync(menuItem);
        }
    }
}
