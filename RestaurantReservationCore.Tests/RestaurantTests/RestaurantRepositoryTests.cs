using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.RestaurantManagement;

namespace RestaurantReservationCore.Tests.RestaurantTests
{
    [Trait("Category", "RepositoryTests")]
    public class RestaurantRepositoryTests
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly RestaurantRepository _restaurantRepository;
        private readonly IFixture _fixture;

        public RestaurantRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantReservationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantReservationDbContext(options);

            _restaurantRepository = new RestaurantRepository(_context);

            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllRestaurantsAsync_ShouldReturnAllRestaurants()
        {
            // Arrange
            var restaurants = _fixture.CreateMany<Restaurant>(10).ToList();
            await _context.Restaurants.AddRangeAsync(restaurants);
            await _context.SaveChangesAsync();

            // Act
            var result = await _restaurantRepository.GetAllAsync();

            // Assert
            Assert.Equal(restaurants.Count, result.Count);
        }

        [Fact]
        public async Task GetRestaurantByIdAsync_ShouldReturnRestaurant()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _restaurantRepository.GetByIdAsync(restaurant.RestaurantId);

            // Assert
            Assert.Equal(restaurant.RestaurantId, result.RestaurantId);
        }

        [Fact]
        public async Task AddRestaurantAsync_ShouldAddRestaurant()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();

            // Act
            await _restaurantRepository.AddAsync(restaurant);

            // Assert
            var result = await _context.Restaurants.FindAsync(restaurant.RestaurantId);
            Assert.Equal(restaurant.RestaurantId, result.RestaurantId);
        }

        [Fact]
        public async Task UpdateRestaurantAsync_ShouldUpdateRestaurant()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();

            restaurant.Name = "Updated Name";

            // Act
            await _restaurantRepository.UpdateAsync(restaurant);

            // Assert
            Assert.Equal("Updated Name", restaurant.Name);
        }

        [Fact]
        public async Task DeleteRestaurantAsync_ShouldDeleteRestaurant()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();

            // Act
            await _restaurantRepository.DeleteAsync(restaurant);

            // Assert
            var result = await _context.Restaurants.FindAsync(restaurant.RestaurantId);
            Assert.Null(result);
        }
    }
}