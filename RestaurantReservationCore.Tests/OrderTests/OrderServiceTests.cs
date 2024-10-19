using AutoFixture;
using Moq;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.OrderManagement;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.Tests.OrderTests
{
    [Trait("Category", "ServiceTests")]
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly OrderService _orderService;
        private readonly IFixture fixture;
        public OrderServiceTests()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_orderRepository.Object);
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task AddOrderAsync_ShouldAddOrder_WhenOrderDoesNotExist()
        {
            // Arrange
            var order = fixture.Create<Order>();
            _orderRepository.Setup(repo => repo.GetByIdAsync(order.OrderId)).ReturnsAsync((Order)null);

            // Act
            await _orderService.AddOrderAsync(order);

            // Assert
            _orderRepository.Verify(repo => repo.AddAsync(order), Times.Once);
        }

        [Fact]
        public async Task AddOrderAsync_ShouldNotAddOrder_WhenOrderAlreadyExists()
        {
            // Arrange
            var order = fixture.Create<Order>();
            _orderRepository.Setup(repo => repo.GetByIdAsync(order.OrderId)).ReturnsAsync(order);

            // Act
            await _orderService.AddOrderAsync(order);

            // Assert
            _orderRepository.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShouldReturnAllOrders()
        {
            // Arrange
            var orders = fixture.CreateMany<Order>(10).ToList();
            _orderRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _orderService.GetAllOrdersAsync();
            var output = stringWriter.ToString();
            Console.WriteLine(output);

            // Assert
            _orderRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            foreach(var order in orders)
            {
                Assert.Contains(order.OrderId.ToString(), output);
            }
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShoulNotdReturnAllOrders()
        {
            // Arrange
            var orders = fixture.CreateMany<Order>(10).ToList();
            var emptyList = new List<Order>();
            _orderRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyList);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _orderService.GetAllOrdersAsync();
            var output = stringWriter.ToString();
            Console.WriteLine(output);

            // Assert
            _orderRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.Contains("No orders found", output);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder()
        {
            // Arrange
            var order = fixture.Create<Order>();
            _orderRepository.Setup(repo => repo.GetByIdAsync(order.OrderId)).ReturnsAsync(order);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _orderService.GetOrderByIdAsync(order.OrderId);
            var output = stringWriter.ToString();
            Console.WriteLine(output);

            // Assert
            _orderRepository.Verify(repo => repo.GetByIdAsync(order.OrderId), Times.Once);
            Assert.Contains(order.OrderId.ToString(), output);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldNotReturnOrder()
        {
            // Arrange
            var order = fixture.Create<Order>();
            var emptyOrder = (Order)null;
            _orderRepository.Setup(repo => repo.GetByIdAsync(order.OrderId)).ReturnsAsync(emptyOrder);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _orderService.GetOrderByIdAsync(order.OrderId);
            var output = stringWriter.ToString();
            Console.WriteLine(output);

            // Assert
            _orderRepository.Verify(repo => repo.GetByIdAsync(order.OrderId), Times.Once);
            Assert.Contains("Order not found", output);
        }

        [Fact]
        public async Task UpdateOrderAsync_ShouldUpdateOrder()
        {
            // Arrange
            var order = fixture.Create<Order>();
            _orderRepository.Setup(repo => repo.GetByIdAsync(order.OrderId)).ReturnsAsync(order);

            // Act
            await _orderService.UpdateOrderAsync(order.OrderId, order);

            // Assert
            _orderRepository.Verify(repo => repo.UpdateAsync(order), Times.Once);
        }

        [Fact]
        public async Task UpdateOrderAsync_ShouldNotUpdateOrder()
        {
            // Arrange
            var order = fixture.Create<Order>();
            _orderRepository.Setup(repo => repo.GetByIdAsync(order.OrderId)).ReturnsAsync((Order)null);

            // Act
            await _orderService.UpdateOrderAsync(order.OrderId, order);

            // Assert
            _orderRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldDeleteOrder()
        {
            // Arrange
            var order = fixture.Create<Order>();
            _orderRepository.Setup(repo => repo.GetByIdAsync(order.OrderId)).ReturnsAsync(order);

            // Act
            await _orderService.DeleteOrderAsync(order.OrderId);

            // Assert
            _orderRepository.Verify(repo => repo.DeleteAsync(order), Times.Once);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldNotDeleteOrder()
        {
            // Arrange
            var order = fixture.Create<Order>();
            _orderRepository.Setup(repo => repo.GetByIdAsync(order.OrderId)).ReturnsAsync((Order)null);

            // Act
            await _orderService.DeleteOrderAsync(order.OrderId);

            // Assert
            _orderRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Order>()), Times.Never);
        }
    }
}
