using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.UI
{
    public class MenuItemUI
    {

        private readonly MenuItemService _menuItemService;

        public MenuItemUI(MenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        public void DisplayOptions()
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
                if (Convert.ToInt32(input) == 6)
                {
                    ListOrderedMenuItems();
                }
                try
                {
                    Enum.TryParse(input, out OperationOptions option);
                    if (option == OperationOptions.Exit)
                    {
                        return;
                    }
                    HandleRequest(option);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void HandleRequest(OperationOptions option)
        {
            switch (option)
            {
                case OperationOptions.Add:
                    AddItem();
                    break;
                case OperationOptions.Update:
                    UpdateItem();
                    break;
                case OperationOptions.Delete:
                    DeleteItem();
                    break;
                case OperationOptions.View:
                    ViewAllItems();
                    break;
                case OperationOptions.Search:
                    ViewItemById();
                    break;
            }
        }

        private void ViewItemById()
        {
            Console.WriteLine("Enter Menu Item id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            _menuItemService.GetMenuItemByIdAsync(id).Wait();
        }

        private void ViewAllItems()
        {
            _menuItemService.GetAllMenuItemsAsync();
        }

        private void DeleteItem()
        {
            Console.WriteLine("Enter Menu Item id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            _menuItemService.DeleteMenuItemAsync(id).Wait();
        }

        private void UpdateItem()
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
            _ = _menuItemService.UpdateMenuItemAsync(id, item);
        }

        private void AddItem()
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
            _menuItemService.AddMenuItemAsync(item);
        }

        private void ListOrderedMenuItems()
        {
            Console.WriteLine("Enter Reservation Id: ");
            int reservationId = Convert.ToInt32(Console.ReadLine());
            _menuItemService.GetMenuItemsByReservationIdAsync(reservationId).Wait();
        }
    }
}
