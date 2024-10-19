using AutoFixture;
using Moq;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.EmployeeManagement;
using RestaurantReservationCore.Db.Repositories.MenuItemManagement;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.Tests.MenuItemTests
{
    [Trait("Category", "ServiceTests")]
    public class MenuItemServiceTests
    {

        private readonly Mock<IMenuItemRepository> _menuItemRepository;
        private readonly MenuItemService _menuItemService;
        private readonly IFixture _fixture;

        public MenuItemServiceTests()
        {
            _menuItemRepository = new Mock<IMenuItemRepository>();
            _menuItemService = new MenuItemService(_menuItemRepository.Object);
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task AddMenuItemAsync_ShouldAddMenuItem_WhenMenuItemDoesNotExist()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            _menuItemRepository.Setup(repo => repo.GetByIdAsync(menuItem.MenuItemId)).ReturnsAsync((MenuItem)null);

            // Act
            await _menuItemService.AddMenuItemAsync(menuItem);

            // Assert
            _menuItemRepository.Verify(repo => repo.AddAsync(menuItem), Times.Once);
        }

        [Fact]
        public async Task AddMenuItemAsync_ShouldNotAddMenuItem_WhenMenuItemAlreadyExists()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            _menuItemRepository.Setup(repo => repo.GetByIdAsync(menuItem.MenuItemId)).ReturnsAsync(menuItem);

            // Act
            await _menuItemService.AddMenuItemAsync(menuItem);

            // Assert
            _menuItemRepository.Verify(repo => repo.AddAsync(It.IsAny<MenuItem>()), Times.Never);
        }

        [Fact]
        public async Task GetAllMenuItemsAsync_ShouldReturnAllMenuItems()
        {
            // Arrange
            var menuItems = _fixture.CreateMany<MenuItem>(5).ToList();
            _menuItemRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(menuItems);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _menuItemService.GetAllMenuItemsAsync();
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _menuItemRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            foreach (var menuItem in menuItems)
            {
                Assert.Contains(menuItem.ToString(), output);
            }
        }

        [Fact]
        public async Task GetAllMenuItemsAsync_ShouldNotReturnAllMenuItems()
        {
            // Arrange
            var menuItems = _fixture.CreateMany<MenuItem>(5).ToList();
            var emptyList = new List<MenuItem>();
            _menuItemRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyList);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _menuItemService.GetAllMenuItemsAsync();
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _menuItemRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.Equal("No menu items found", output.Trim());
        }

        [Fact]
        public async Task GetMenuItemByIdAsync_ShouldReturnMenuItem()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            _menuItemRepository.Setup(repo => repo.GetByIdAsync(menuItem.MenuItemId)).ReturnsAsync(menuItem);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _menuItemService.GetMenuItemByIdAsync(menuItem.MenuItemId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _menuItemRepository.Verify(repo => repo.GetByIdAsync(menuItem.MenuItemId), Times.Once);
            Assert.Contains(menuItem.ToString(), output);
        }

        [Fact]
        public async Task GetMenuItemByIdAsync_ShouldNotReturnMenuItem()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            _menuItemRepository.Setup(repo => repo.GetByIdAsync(menuItem.MenuItemId)).ReturnsAsync((MenuItem)null);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _menuItemService.GetMenuItemByIdAsync(menuItem.MenuItemId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _menuItemRepository.Verify(repo => repo.GetByIdAsync(menuItem.MenuItemId), Times.Once);
            Assert.Equal("Menu item not found", output.Trim());
        }

        [Fact]
        public async Task UpdateMenuItemAsync_ShouldUpdateMenuItem()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            _menuItemRepository.Setup(repo => repo.GetByIdAsync(menuItem.MenuItemId)).ReturnsAsync(menuItem);

            // Act
            await _menuItemService.UpdateMenuItemAsync(menuItem.MenuItemId, menuItem);

            // Assert
            _menuItemRepository.Verify(repo => repo.UpdateAsync(menuItem), Times.Once);
        }

        [Fact]
        public async Task UpdateMenuItemAsync_ShouldNotUpdateMenuItem()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            _menuItemRepository.Setup(repo => repo.GetByIdAsync(menuItem.MenuItemId)).ReturnsAsync((MenuItem)null);

            // Act
            await _menuItemService.UpdateMenuItemAsync(menuItem.MenuItemId, menuItem);

            // Assert
            _menuItemRepository.Verify(repo => repo.UpdateAsync(It.IsAny<MenuItem>()), Times.Never);
        }

        [Fact]
        public async Task DeleteMenuItemAsync_ShouldDeleteMenuItem()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            _menuItemRepository.Setup(repo => repo.GetByIdAsync(menuItem.MenuItemId)).ReturnsAsync(menuItem);

            // Act
            await _menuItemService.DeleteMenuItemAsync(menuItem.MenuItemId);

            // Assert
            _menuItemRepository.Verify(repo => repo.DeleteAsync(menuItem), Times.Once);
        }

        [Fact]
        public async Task DeleteMenuItemAsync_ShouldNotDeleteMenuItem()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            _menuItemRepository.Setup(repo => repo.GetByIdAsync(menuItem.MenuItemId)).ReturnsAsync((MenuItem)null);

            // Act
            await _menuItemService.DeleteMenuItemAsync(menuItem.MenuItemId);

            // Assert
            _menuItemRepository.Verify(repo => repo.DeleteAsync(It.IsAny<MenuItem>()), Times.Never);
        }
    }
}
