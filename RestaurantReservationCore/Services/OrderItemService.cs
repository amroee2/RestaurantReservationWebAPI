using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;
using RestaurantReservationCore.UI;

namespace RestaurantReservationCore.Services
{
    public class OrderItemService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;

        public OrderItemService(IRepository<OrderItem> orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task GetAllOrderItemsAsync()
        {

            List<OrderItem> orderItems =  await _orderItemRepository.GetAllAsync();
            if (!orderItems.Any())
            {
                Console.WriteLine("No order items found");
                return;
            }
            foreach (var orderItem in orderItems)
            {
                Console.WriteLine(orderItem);
            }
        }

        public async Task GetOrderItemByIdAsync(int id)
        {
            OrderItem orderItem =  await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
            {
                Console.WriteLine("Order item not found");
                return;
            }
            Console.WriteLine(orderItem);
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            OrderItem requestedOrderItem = await _orderItemRepository.GetByIdAsync(orderItem.OrderItemId);
            if (requestedOrderItem != null)
            {
                Console.WriteLine("Order item already exists");
                return;
            }
            await _orderItemRepository.AddAsync(orderItem);
        }

        public async Task UpdateOrderItemAsync(int id, OrderItem orderItem)
        {
            OrderItem updatedOrderItem = await _orderItemRepository.GetByIdAsync(id);
            if (updatedOrderItem == null)
            {
                Console.WriteLine("Order Item not found");
                return;
            }
            updatedOrderItem.Quantity = orderItem.Quantity;
            updatedOrderItem.MenuItem = orderItem.MenuItem;
            updatedOrderItem.OrderId = orderItem.OrderId;
            await _orderItemRepository.UpdateAsync(updatedOrderItem);
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
            {
                Console.WriteLine("Order item doesn't exist");
                return;
            }
            await _orderItemRepository.DeleteAsync(orderItem);
        }
    }
}
