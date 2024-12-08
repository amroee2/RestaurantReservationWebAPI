using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.MenuItemManagement;
using RestaurantReservationServices.DTOs.MenuItemDTOs;
using RestaurantReservationServices.Exceptions;

namespace RestaurantReservationServices.Services.MenuItemManagementService
{
    public class MenuItemService: IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public MenuItemService(IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuItemReadDTO>> GetAllMenuItemsAsync()
        {
            var menuItems = await _menuItemRepository.GetAllAsync();
            return _mapper.Map<List<MenuItemReadDTO>>(menuItems);
        }

        public async Task<MenuItemReadDTO> GetMenuItemByIdAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
            {
                throw new EntityNotFoundException("Menu Item Not Found");
            }
            return _mapper.Map<MenuItemReadDTO>(menuItem);
        }

        public async Task<int> AddMenuItemAsync(MenuItemCreateDTO menuItem)
        {
            var newMenuItem = _mapper.Map<MenuItem>(menuItem);
            await _menuItemRepository.AddAsync(newMenuItem);
            return newMenuItem.MenuItemId;
        }

        public async Task UpdateMenuItemAsync(int id, MenuItemUpdateDTO menuItem)
        {
            var menuItemToUpdate = await _menuItemRepository.GetByIdAsync(id);
            _mapper.Map(menuItem, menuItemToUpdate);
            await _menuItemRepository.UpdateAsync(menuItemToUpdate);
        }

        public async Task DeleteMenuItemAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            await _menuItemRepository.DeleteAsync(menuItem);
        }

        public async Task<List<MenuItemReadDTO>> GetMenuItemsByReservationIdAsync(int reservationId)
        {
            var menuItems = await _menuItemRepository.GetMenuItemsByReservationIdAsync(reservationId);
            return _mapper.Map<List<MenuItemReadDTO>>(menuItems);
        }
    }
}