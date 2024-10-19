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

        public async Task DisplayOptionsAsync()
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
            await _orderItemService.GetOrderItemByIdAsync(id);
        }

        private async Task ViewAllItemsAsync()
        {
            await _orderItemService.GetAllOrderItemsAsync();
        }

        private async Task DeleteItemAsync()
        {
            Console.WriteLine("Enter Menu Item id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            await _orderItemService.DeleteOrderItemAsync(id);
        }

        private async Task UpdateItemAsync()
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
            await _orderItemService.UpdateOrderItemAsync(id, item);
        }

        private async Task AddItemAsync()
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
            await _orderItemService.AddOrderItemAsync(item);
        }
    }
}
