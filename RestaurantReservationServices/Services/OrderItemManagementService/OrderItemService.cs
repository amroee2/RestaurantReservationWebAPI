using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;
using RestaurantReservationServices.DTOs.OrderItemDTOs;
using RestaurantReservationServices.Exceptions;

namespace RestaurantReservationServices.Services.OrderItemManagementService
{
    public class OrderItemService: IOrderItemService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IMapper _mapper;
        public OrderItemService(IRepository<OrderItem> orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderItemReadDTO>> GetAllOrderItemsAsync()
        {
            var orderItems = await _orderItemRepository.GetAllAsync();
            return _mapper.Map<List<OrderItemReadDTO>>(orderItems);
        }

        public async Task<OrderItemReadDTO> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
            {
                throw new EntityNotFoundException("Order item not found");
            }
            return _mapper.Map<OrderItemReadDTO>(orderItem);
        }

        public async Task<int> AddOrderItemAsync(OrderItemCreateDTO orderItem)
        {
            var newOrderItem = _mapper.Map<OrderItem>(orderItem);
            await _orderItemRepository.AddAsync(newOrderItem);
            return newOrderItem.OrderItemId;
        }

        public async Task UpdateOrderItemAsync(int id, OrderItemUpdateDTO orderItem)
        {
            var orderItemToUpdate = await _orderItemRepository.GetByIdAsync(id);
            _mapper.Map(orderItem, orderItemToUpdate);
            await _orderItemRepository.UpdateAsync(orderItemToUpdate);
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            await _orderItemRepository.DeleteAsync(orderItem);
        }
    }
}