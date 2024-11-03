using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;

namespace RestaurantReservationCore.Tests.OrderItemTests
{
    [Trait("Category", "RepositoryTests")]
    public class OrderItemRepositoryTests
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly IFixture _fixture;

        public OrderItemRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantReservationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantReservationDbContext(options);

            _orderItemRepository = new OrderItemRepository(_context);

            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllOrderItemsAsync_ShouldReturnAllOrderItems()
        {
            // Arrange
            var orderItems = _fixture.CreateMany<OrderItem>(10).ToList();
            await _context.OrderItems.AddRangeAsync(orderItems);
            await _context.SaveChangesAsync();

            // Act
            var result = await _orderItemRepository.GetAllAsync();

            // Assert
            Assert.Equal(orderItems.Count, result.Count);
        }

        [Fact]
        public async Task GetOrderItemByIdAsync_ShouldReturnOrderItem()
        {
            // Arrange
            var orderItem = _fixture.Create<OrderItem>();
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            // Act
            var result = await _orderItemRepository.GetByIdAsync(orderItem.OrderItemId);

            // Assert
            Assert.Equal(orderItem.OrderItemId, result.OrderItemId);
        }

        [Fact]
        public async Task AddOrderItemAsync_ShouldAddOrderItem()
        {
            // Arrange
            var orderItem = _fixture.Create<OrderItem>();

            // Act
            await _orderItemRepository.AddAsync(orderItem);

            // Assert
            var result = await _context.OrderItems.FindAsync(orderItem.OrderItemId);
            Assert.Equal(orderItem.OrderItemId, result.OrderItemId);
        }

        [Fact]
        public async Task UpdateOrderItemAsync_ShouldUpdateOrderItem()
        {
            // Arrange
            var orderItem = _fixture.Create<OrderItem>();
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            orderItem.Quantity = 10;
            // Act
            await _orderItemRepository.UpdateAsync(orderItem);

            // Assert
            Assert.Equal(10, orderItem.Quantity);
        }

        [Fact]
        public async Task DeleteOrderItemAsync_ShouldDeleteOrderItem()
        {
            // Arrange
            var orderItem = _fixture.Create<OrderItem>();
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            // Act
            await _orderItemRepository.DeleteAsync(orderItem);

            // Assert
            var result = await _context.OrderItems.FindAsync(orderItem.OrderItemId);
            Assert.Null(result);
        }
    }
}