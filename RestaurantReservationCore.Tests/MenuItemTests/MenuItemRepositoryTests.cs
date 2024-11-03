using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.MenuItemManagement;

namespace RestaurantReservationCore.Tests.MenuItemTests
{
    [Trait("Category", "RepositoryTests")]
    public class MenuItemRepositoryTests
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly MenuItemRepository _menuItemRepository;
        private readonly IFixture _fixture;

        public MenuItemRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantReservationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantReservationDbContext(options);

            _menuItemRepository = new MenuItemRepository(_context);

            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllMenuItemsAsync_ShouldReturnAllMenuItems()
        {
            // Arrange
            var menuItems = _fixture.CreateMany<MenuItem>(10).ToList();
            await _context.MenuItems.AddRangeAsync(menuItems);
            await _context.SaveChangesAsync();

            // Act
            var result = await _menuItemRepository.GetAllAsync();

            // Assert
            Assert.Equal(menuItems.Count, result.Count);
        }

        [Fact]
        public async Task GetMenuItemByIdAsync_ShouldReturnMenuItem()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();

            // Act
            var result = await _menuItemRepository.GetByIdAsync(menuItem.MenuItemId);

            // Assert
            Assert.Equal(menuItem.MenuItemId, result.MenuItemId);
        }

        [Fact]
        public async Task AddMenuItemAsync_ShouldAddMenuItem()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();

            // Act
            await _menuItemRepository.AddAsync(menuItem);

            // Assert
            var result = await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == menuItem.MenuItemId);
            Assert.Equal(menuItem.MenuItemId, result.MenuItemId);
        }

        [Fact]
        public async Task UpdateMenuItemAsync_ShouldUpdateMenuItem()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();

            // Act
            menuItem.Name = "Updated Name";
            await _menuItemRepository.UpdateAsync(menuItem);

            // Assert
            var result = await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == menuItem.MenuItemId);
            Assert.Equal("Updated Name", result.Name);
        }

        [Fact]
        public async Task DeleteMenuItemAsync_ShouldDeleteMenuItem()
        {
            // Arrange
            var menuItem = _fixture.Create<MenuItem>();
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();

            // Act
            await _menuItemRepository.DeleteAsync(menuItem);

            // Assert
            var result = await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == menuItem.MenuItemId);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetMenuItemsByReservationIdAsync_ShouldReturnMenuItems()
        {
            // Arrange
            var reservationId = 1;
            var order = _fixture.Build<Order>()
                .With(o=>o.ReservationId, reservationId)
                .Without(o => o.Reservation)
                .Create();
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            var menuItem = _fixture.Create<MenuItem>();
            var orderItem = new OrderItem
            {
                MenuItem = menuItem,
                Order = order
            };
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            var menuItems = await _context.Orders.Where(o => o.ReservationId == reservationId)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.MenuItem)
                .SelectMany(order => order.OrderItems.Select(oi => oi.MenuItem))
                .ToListAsync();

            // Act
            var result = await _menuItemRepository.GetMenuItemsByReservationIdAsync(reservationId);

            // Assert
            Assert.Equal(menuItems.Count(), result.Count);
        }
    }
}