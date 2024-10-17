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

        public void DisplayOptions()
        {
            while (true)
            {
                Console.WriteLine("1. Add Order");
                Console.WriteLine("2. Update Order");
                Console.WriteLine("3. Delete Order");
                Console.WriteLine("4. View All Orders");
                Console.WriteLine("5. View Order By Id");
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
            _orderService.GetOrderByIdAsync(id).Wait();
        }

        private void ViewAllItems()
        {
            _orderService.GetAllOrdersAsync();
        }

        private void DeleteItem()
        {
            Console.WriteLine("Enter Menu Item id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            _orderService.DeleteOrderAsync(id).Wait();
        }

        private void UpdateItem()
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
            _orderService.UpdateOrderAsync(id, order);
        }

        private void AddItem()
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
            _orderService.AddOrderAsync(order);
        }
    }
}
