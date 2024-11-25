using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.OrderManagement;

namespace RestaurantReservationServices.Services.OrderManagementService
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task GetAllOrdersAsync()
        {
            List<Order> orders = await _orderRepository.GetAllAsync();
            if (!orders.Any())
            {
                Console.WriteLine("No orders found");
                return;
            }
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }

        public async Task GetOrderByIdAsync(int id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                Console.WriteLine("Order not found");
                return;
            }
            Console.WriteLine(order);
        }

        public async Task AddOrderAsync(Order order)
        {
            Order requestedOrder = await _orderRepository.GetByIdAsync(order.OrderId);
            if (requestedOrder != null)
            {
                Console.WriteLine("Order already exists");
                return;
            }
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderAsync(int id, Order order)
        {
            Order updatedOrder = await _orderRepository.GetByIdAsync(id);
            if (updatedOrder == null)
            {
                Console.WriteLine("Order not found");
                return;
            }
            updatedOrder.OrderDate = order.OrderDate;
            updatedOrder.TotalAmount = order.TotalAmount;
            updatedOrder.ReservationId = order.ReservationId;
            updatedOrder.EmployeeId = order.EmployeeId;
            await _orderRepository.UpdateAsync(updatedOrder);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                Console.WriteLine("Order doesn't exist");
                return;
            }
            await _orderRepository.DeleteAsync(order);
        }

        public async Task GetOrdersByReservationIdAsync(int reservationId)
        {
            List<Order> orders = await _orderRepository.GetOrdersByReservationIdAsync(reservationId);
            if (!orders.Any())
            {
                Console.WriteLine("No orders found");
                return;
            }
            foreach (var order in orders)
            {
                Console.WriteLine(order);
                foreach (var item in order.OrderItems)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}