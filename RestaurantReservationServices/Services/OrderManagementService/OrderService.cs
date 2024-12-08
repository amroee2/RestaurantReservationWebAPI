using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.OrderManagement;
using RestaurantReservationServices.DTOs.OrderDTOs;
using RestaurantReservationServices.Exceptions;

namespace RestaurantReservationServices.Services.OrderManagementService
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderReadDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderReadDTO>>(orders);
        }

        public async Task<OrderReadDTO> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new EntityNotFoundException("Order not found");
            }
            return _mapper.Map<OrderReadDTO>(order);
        }

        public async Task<int> AddOrderAsync(OrderCreateDTO order)
        {
            var newOrder = _mapper.Map<Order>(order);
            await _orderRepository.AddAsync(newOrder);
            return newOrder.OrderId;
        }

        public async Task UpdateOrderAsync(int id, OrderUpdateDTO order)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(id);
            _mapper.Map(order, orderToUpdate);
            await _orderRepository.UpdateAsync(orderToUpdate);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            await _orderRepository.DeleteAsync(order);
        }

        public async Task<List<OrderReadDTO>> GetOrdersByReservationIdAsync(int reservationId)
        {
            var orders = await _orderRepository.GetOrdersByReservationIdAsync(reservationId);
            return _mapper.Map<List<OrderReadDTO>>(orders);
        }
    }
}