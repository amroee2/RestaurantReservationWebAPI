using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;
using System.Runtime.CompilerServices;

namespace RestaurantReservationCore.UI
{
    public class MenuItemUI
    {

        private readonly MenuItemService _menuItemService;

        public MenuItemUI(MenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        public async Task DisplayOptionsAsync()
        {
            while (true)
            {
                const int listOrderedMenuItems = 6;
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Update Item");
                Console.WriteLine("3. Delete Item");
                Console.WriteLine("4. View All Items");
                Console.WriteLine("5. View Item By Id");
                Console.WriteLine("6. List Ordered Menu Items");
                Console.WriteLine("0. Go Back");

                string input = Console.ReadLine();
                if (Convert.ToInt32(input) == listOrderedMenuItems)
                {
                    await ListOrderedMenuItemsAsync();
                }
                try
                {
                    Enum.TryParse(input, out OperationOptions option);
                    if (option == OperationOptions.Exit)
                    {
                        return;
                    }
                    await HandleRequestAsync(option);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task HandleRequestAsync(OperationOptions option)
        {
            switch (option)
            {
                case OperationOptions.Add:
                    await AddItemAsync();
                    break;
                case OperationOptions.Update:
                    await UpdateItemAsync();
                    break;
                case OperationOptions.Delete:
                    await DeleteItemAsync();
                    break;
                case OperationOptions.View:
                    await ViewAllItemsAsync();
                    break;
                case OperationOptions.Search:
                    await ViewItemByIdAsync();
                    break;
            }
        }

        private async Task ViewItemByIdAsync()
        {
            Console.WriteLine("Enter Menu Item id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            await _menuItemService.GetMenuItemByIdAsync(id);
        }

        private async Task ViewAllItemsAsync()
        {
            await _menuItemService.GetAllMenuItemsAsync();
        }

        private async Task DeleteItemAsync()
        {
            Console.WriteLine("Enter Menu Item id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            await _menuItemService.DeleteMenuItemAsync(id);
        }

        private async Task UpdateItemAsync()
        {
            Console.WriteLine("Enter Menu Item id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Menu Item name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Menu Item description: ");
            string description = Console.ReadLine();
            Console.WriteLine("Enter Menu Item Price");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Restaurant Id: ");
            int restaurantId = Convert.ToInt32(Console.ReadLine());
            MenuItem item = new MenuItem
            {
                Name = name,
                Description = description,
                Price = price,
                RestaurantId = restaurantId
            };
            await _menuItemService.UpdateMenuItemAsync(id, item);
        }

        private async Task AddItemAsync()
        {
            Console.WriteLine("Enter Menu Item name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Menu Item description: ");
            string description = Console.ReadLine();
            Console.WriteLine("Enter Menu Item Price");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Restaurant Id: ");
            int restaurantId = Convert.ToInt32(Console.ReadLine());
            MenuItem item = new MenuItem
            {
                Name = name,
                Description = description,
                Price = price,
                RestaurantId = restaurantId
            };
            await _menuItemService.AddMenuItemAsync(item);
        }

        private async Task ListOrderedMenuItemsAsync()
        {
            Console.WriteLine("Enter Reservation Id: ");
            int reservationId = Convert.ToInt32(Console.ReadLine());
            await _menuItemService.GetMenuItemsByReservationIdAsync(reservationId);
        }
    }
}
