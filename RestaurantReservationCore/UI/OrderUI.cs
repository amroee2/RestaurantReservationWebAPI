using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.UI
{
    public class OrderUI
    {
        private readonly OrderService _orderService;

        public OrderUI(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task DisplayOptionsAsync()
        {
            while (true)
            {
                const int listOrderAndMenuItemsOption = 6;
                Console.WriteLine("1. Add Order");
                Console.WriteLine("2. Update Order");
                Console.WriteLine("3. Delete Order");
                Console.WriteLine("4. View All Orders");
                Console.WriteLine("5. View Order By Id");
                Console.WriteLine("6. List Order And Menu Items");
                Console.WriteLine("0. Go Back");

                string input = Console.ReadLine();
                if(Convert.ToInt32(input) == listOrderAndMenuItemsOption)
                {
                    await ListOrderAndMenuItemsAsync();
                    continue;
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
            await _orderService.GetOrderByIdAsync(id);
        }

        private async Task ViewAllItemsAsync()
        {
            await _orderService.GetAllOrdersAsync();
        }

        private async Task DeleteItemAsync()
        {
            Console.WriteLine("Enter Menu Item id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            await _orderService.DeleteOrderAsync(id);
        }

        private async Task UpdateItemAsync()
        {
            Console.WriteLine("Enter Order Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Order Date");
            DateTime orderDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Total Amount");
            double totalAmount = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Employee Id");
            int employeeId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Reservation Id");
            int reservationId = Convert.ToInt32(Console.ReadLine());
            Order order = new Order
            {
                OrderId = id,
                OrderDate = orderDate,
                TotalAmount = totalAmount,
                EmployeeId = employeeId,
                ReservationId = reservationId
            };
            await _orderService.UpdateOrderAsync(id, order);
        }

        private async Task AddItemAsync()
        {
            Console.WriteLine("Enter Order Date");
            DateTime orderDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Total Amount");
            double totalAmount = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Employee Id");
            int employeeId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Reservation Id");
            int reservationId = Convert.ToInt32(Console.ReadLine());
            Order order = new Order
            {
                OrderDate = orderDate,
                TotalAmount = totalAmount,
                EmployeeId = employeeId,
                ReservationId = reservationId
            };
            await _orderService.AddOrderAsync(order);
        }

        private async Task ListOrderAndMenuItemsAsync()
        {
            Console.WriteLine("Enter Reservation Id");
            int id = Convert.ToInt32(Console.ReadLine());
            await _orderService.GetOrdersByReservationIdAsync(id);
        }
    }
}
