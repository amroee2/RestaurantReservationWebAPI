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
        private readonly IFixture _fixture;
        public OrderServiceTests()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_orderRepository.Object);
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task AddOrderAsync_ShouldAddOrder_WhenOrderDoesNotExist()
        {
            // Arrange
            var order = _fixture.Create<Order>();
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
            var order = _fixture.Create<Order>();
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
            var orders = _fixture.CreateMany<Order>(10).ToList();
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
            var orders = _fixture.CreateMany<Order>(10).ToList();
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
            var order = _fixture.Create<Order>();
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
            var order = _fixture.Create<Order>();
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
            var order = _fixture.Create<Order>();
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
            var order = _fixture.Create<Order>();
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
            var order = _fixture.Create<Order>();
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
            var order = _fixture.Create<Order>();
            _orderRepository.Setup(repo => repo.GetByIdAsync(order.OrderId)).ReturnsAsync((Order)null);

            // Act
            await _orderService.DeleteOrderAsync(order.OrderId);

            // Assert
            _orderRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Order>()), Times.Never);
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
            _orderRepository.Setup(repo => repo.GetOrdersByReservationIdAsync(reservationId)).ReturnsAsync(orders);
            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _orderService.GetOrdersByReservationIdAsync(orders[0].ReservationId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _orderRepository.Verify(repo => repo.GetOrdersByReservationIdAsync(orders[0].ReservationId), Times.Once);
            foreach (var order in orders)
            {
                Assert.Contains(order.OrderId.ToString(), output);
                foreach(var item in order.OrderItems)
                {
                    Assert.Contains(item.ToString(), output);
                }
            }
        }

        [Fact]
        public async Task GetOrdersByReservationIdAsync_ShouldNotReturnOrders()
        {
            // Arrange
            var reservationId = 1;
            var orders = new List<Order>();
            _orderRepository.Setup(repo => repo.GetOrdersByReservationIdAsync(reservationId)).ReturnsAsync(orders);
            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _orderService.GetOrdersByReservationIdAsync(reservationId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _orderRepository.Verify(repo => repo.GetOrdersByReservationIdAsync(reservationId), Times.Once);
            Assert.Contains("No orders found", output);
        }
    }
}
