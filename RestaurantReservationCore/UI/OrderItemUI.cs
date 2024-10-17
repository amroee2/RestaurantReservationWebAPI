using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.UI
{
    public class OrderItemUI
    {

        private readonly OrderItemService _orderItemService;

        public OrderItemUI(OrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        public void DisplayOptions()
        {
            while (true)
            {
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Update Item");
                Console.WriteLine("3. Delete Item");
                Console.WriteLine("4. View All Items");
                Console.WriteLine("5. View Item By Id");
                Console.WriteLine("0. Go Back");

                try
                {
                    Enum.TryParse(Console.ReadLine(), out OperationOptions option);
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
            _orderItemService.GetOrderItemByIdAsync(id).Wait();
        }

        private void ViewAllItems()
        {
            _orderItemService.GetAllOrderItemsAsync();
        }

        private void DeleteItem()
        {
            Console.WriteLine("Enter Menu Item id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            _orderItemService.DeleteOrderItemAsync(id).Wait();
        }

        private void UpdateItem()
        {
            Console.WriteLine("Enter Order Item id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Order Item quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Menu Item Id: ");
            int menuItemId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Order Item Id");
            int orderItem = Convert.ToInt32(Console.ReadLine());
            OrderItem item = new OrderItem
            {
                Quantity = quantity,
                MenuItemId = menuItemId,
                OrderId = orderItem
            };
            _ = _orderItemService.UpdateOrderItemAsync(id, item);
        }

        private void AddItem()
        {
            Console.WriteLine("Enter Order Item quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Menu Item Id: ");
            int menuItemId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Order Item Id");
            int orderItem = Convert.ToInt32(Console.ReadLine());
            OrderItem item = new OrderItem
            {
                Quantity = quantity,
                MenuItemId = menuItemId,
                OrderId = orderItem
            };
            _orderItemService.AddOrderItemAsync(item);
        }
    }
}
