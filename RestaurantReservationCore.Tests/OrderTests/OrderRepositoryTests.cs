using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.OrderManagement;

namespace RestaurantReservationCore.Tests.OrderTests
{
    [Trait("Category", "RepositoryTests")]
    public class OrderRepositoryTests
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly OrderRepository _orderRepository;
        private readonly IFixture _fixture;

        public OrderRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantReservationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantReservationDbContext(options);

            _orderRepository = new OrderRepository(_context);

            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShouldReturnAllOrders()
        {
            // Arrange
            var orders = _fixture.CreateMany<Order>(10).ToList();
            await _context.Orders.AddRangeAsync(orders);
            await _context.SaveChangesAsync();

            // Act
            var result = await _orderRepository.GetAllAsync();

            // Assert
            Assert.Equal(orders.Count, result.Count);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder()
        {
            // Arrange
            var order = _fixture.Create<Order>();
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _orderRepository.GetByIdAsync(order.OrderId);

            // Assert
            Assert.Equal(order.OrderId, result.OrderId);
        }

        [Fact]
        public async Task AddOrderAsync_ShouldAddOrder()
        {
            // Arrange
            var order = _fixture.Create<Order>();

            // Act
            await _orderRepository.AddAsync(order);

            // Assert
            var result = await _context.Orders.FindAsync(order.OrderId);
            Assert.Equal(order.OrderId, result.OrderId);
        }

        [Fact]
        public async Task UpdateOrderAsync_ShouldUpdateOrder()
        {
            // Arrange
            var order = _fixture.Create<Order>();
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            order.TotalAmount = 10;

            // Act
            await _orderRepository.UpdateAsync(order);

            // Assert
            Assert.Equal(10, order.TotalAmount);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldDeleteOrder()
        {
            // Arrange
            var order = _fixture.Create<Order>();
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Act
            await _orderRepository.DeleteAsync(order);

            // Assert
            var result = await _context.Orders.FindAsync(order.OrderId);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetOrdersByReservationIdAsync_ShouldReturnOrders()
        {
            // Arrange
            var reservationId = 1;
            var orders = _fixture.Build<Order>()
                .With(o => o.ReservationId, reservationId)
                .Without(o => o.Reservation)
                .CreateMany(5)
                .ToList();
            await _context.Orders.AddRangeAsync(orders);
            await _context.SaveChangesAsync();

            // Act
            var result = await _orderRepository.GetOrdersByReservationIdAsync(reservationId);

            // Assert
            Assert.Equal(orders.Count, result.Count);
        }
    }
}