using AutoFixture;
using Moq;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.Tests.OrderItemTests
{
    [Trait("Category", "ServiceTests")]
    public class OrderItemServiceTests
    {
        private readonly Mock<IRepository<OrderItem>> _orderItemRepository;
        private readonly OrderItemService _orderItemService;
        private readonly IFixture fixture;

        public OrderItemServiceTests()
        {
            _orderItemRepository = new Mock<IRepository<OrderItem>>();
            _orderItemService = new OrderItemService(_orderItemRepository.Object);
            fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task AddOrderItemAsync_ShouldAddOrderItem_WhenOrderItemDoesNotExist()
        {
            // Arrange
            var orderItem = fixture.Create<OrderItem>();
            _orderItemRepository.Setup(repo => repo.GetByIdAsync(orderItem.OrderItemId)).ReturnsAsync((OrderItem)null);

            // Act
            await _orderItemService.AddOrderItemAsync(orderItem);

            // Assert
            _orderItemRepository.Verify(repo => repo.AddAsync(orderItem), Times.Once);
        }

        [Fact]
        public async Task AddOrderItemAsync_ShouldNotAddOrderItem_WhenOrderItemAlreadyExists()
        {
            // Arrange
            var orderItem = fixture.Create<OrderItem>();
            _orderItemRepository.Setup(repo => repo.GetByIdAsync(orderItem.OrderItemId)).ReturnsAsync(orderItem);

            // Act
            await _orderItemService.AddOrderItemAsync(orderItem);

            // Assert
            _orderItemRepository.Verify(repo => repo.AddAsync(It.IsAny<OrderItem>()), Times.Never);
        }

        [Fact]
        public async Task GetAllOrderItemsAsync_ShouldReturnAllOrderItems()
        {
            // Arrange
            var orderItems = fixture.CreateMany<OrderItem>().ToList();
            _orderItemRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orderItems);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _orderItemService.GetAllOrderItemsAsync();
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _orderItemRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            foreach (var orderItem in orderItems)
            {
                Assert.Contains(orderItem.ToString(), output);
            }
        }

        [Fact]
        public async Task GetAllOrderItemsAsync_ShouldNotReturnAllOrderItems()
        {
            // Arrange
            var orderItems = fixture.CreateMany<OrderItem>().ToList();
            var emptyList = new List<OrderItem>();
            _orderItemRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyList);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _orderItemService.GetAllOrderItemsAsync();
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _orderItemRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.Contains("No order items found", output);
        }

        [Fact]
        public async Task GetOrderItemByIdAsync_ShouldReturnOrderItem_WhenOrderItemExists()
        {
            // Arrange
            var orderItem = fixture.Create<OrderItem>();
            _orderItemRepository.Setup(repo => repo.GetByIdAsync(orderItem.OrderItemId)).ReturnsAsync(orderItem);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _orderItemService.GetOrderItemByIdAsync(orderItem.OrderItemId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _orderItemRepository.Verify(repo => repo.GetByIdAsync(orderItem.OrderItemId), Times.Once);
            Assert.Contains(orderItem.ToString(), output);
        }

        [Fact]
        public async Task GetOrderItemByIdAsync_ShouldNotReturnOrderItem_WhenOrderItemDoesNotExist()
        {
            // Arrange
            var orderItem = fixture.Create<OrderItem>();
            _orderItemRepository.Setup(repo => repo.GetByIdAsync(orderItem.OrderItemId)).ReturnsAsync((OrderItem)null);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _orderItemService.GetOrderItemByIdAsync(orderItem.OrderItemId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _orderItemRepository.Verify(repo => repo.GetByIdAsync(orderItem.OrderItemId), Times.Once);
            Assert.Equal("Order item not found", output.Trim());
        }

        [Fact]
        public async Task UpdateOrderItemAsync_ShouldUpdateOrderItem_WhenOrderItemExists()
        {
            // Arrange
            var orderItem = fixture.Create<OrderItem>();
            var updatedOrderItem = fixture.Create<OrderItem>();
            _orderItemRepository.Setup(repo => repo.GetByIdAsync(orderItem.OrderItemId)).ReturnsAsync(orderItem);

            // Act
            await _orderItemService.UpdateOrderItemAsync(orderItem.OrderItemId, updatedOrderItem);

            // Assert
            _orderItemRepository.Verify(repo => repo.UpdateAsync(orderItem), Times.Once);
        }

        [Fact]
        public async Task UpdateOrderItemAsync_ShouldNotUpdateOrderItem_WhenOrderItemDoesNotExist()
        {
            // Arrange
            var orderItem = fixture.Create<OrderItem>();
            var updatedOrderItem = fixture.Create<OrderItem>();
            _orderItemRepository.Setup(repo => repo.GetByIdAsync(orderItem.OrderItemId)).ReturnsAsync((OrderItem)null);

            // Act
            await _orderItemService.UpdateOrderItemAsync(orderItem.OrderItemId, updatedOrderItem);

            // Assert
            _orderItemRepository.Verify(repo => repo.UpdateAsync(It.IsAny<OrderItem>()), Times.Never);
        }

        [Fact]
        public async Task DeleteOrderItemAsync_ShouldDeleteOrderItem_WhenOrderItemExists()
        {
            // Arrange
            var orderItem = fixture.Create<OrderItem>();
            _orderItemRepository.Setup(repo => repo.GetByIdAsync(orderItem.OrderItemId)).ReturnsAsync(orderItem);

            // Act
            await _orderItemService.DeleteOrderItemAsync(orderItem.OrderItemId);

            // Assert
            _orderItemRepository.Verify(repo => repo.DeleteAsync(orderItem), Times.Once);
        }

        [Fact]
        public async Task DeleteOrderItemAsync_ShouldNotDeleteOrderItem_WhenOrderItemDoesNotExist()
        {
            // Arrange
            var orderItem = fixture.Create<OrderItem>();
            _orderItemRepository.Setup(repo => repo.GetByIdAsync(orderItem.OrderItemId)).ReturnsAsync((OrderItem)null);

            // Act
            await _orderItemService.DeleteOrderItemAsync(orderItem.OrderItemId);

            // Assert
            _orderItemRepository.Verify(repo => repo.DeleteAsync(It.IsAny<OrderItem>()), Times.Never);
        }
    }
}
