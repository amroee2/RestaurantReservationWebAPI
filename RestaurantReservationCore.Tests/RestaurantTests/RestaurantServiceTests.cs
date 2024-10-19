using AutoFixture;
using Moq;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.RestaurantManagement;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.Tests.RestaurantTests
{
    [Trait("Category", "ServiceTests")]
    public class RestaurantServiceTests
    {
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly RestaurantService _restaurantService;
        private readonly IFixture _fixture;
        public RestaurantServiceTests()
        {
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _restaurantService = new RestaurantService(_mockRestaurantRepository.Object);
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                    .ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task AddRestaurantAsync_ShouldAddRestaurant_WhenRestaurantDoesNotExist()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurant.RestaurantId)).ReturnsAsync((Restaurant)null);

            // Act
            await _restaurantService.AddRestaurantAsync(restaurant);

            // Assert
            _mockRestaurantRepository.Verify(repo => repo.AddAsync(restaurant), Times.Once);
        }

        [Fact]
        public async Task AddRestaurantAsync_ShouldNotAddRestaurant_WhenRestaurantAlreadyExists()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurant.RestaurantId)).ReturnsAsync(restaurant);

            // Act
            await _restaurantService.AddRestaurantAsync(restaurant);

            // Assert
            _mockRestaurantRepository.Verify(repo => repo.AddAsync(It.IsAny<Restaurant>()), Times.Never);
        }

        [Fact]
        public async Task GetAllRestaurantsAsync_ShouldReturnAllRestaurants()
        {
            // Arrange
            var restaurants = _fixture.CreateMany<Restaurant>().ToList();
            _mockRestaurantRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(restaurants);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _restaurantService.GetAllRestaurantsAsync();
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);
            // Assert
            _mockRestaurantRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            foreach (var restaurant in restaurants)
            {
                Assert.Contains(restaurant.RestaurantId.ToString(), output);
            }
        }

        [Fact]
        public async Task GetAllRestaurantsAsync_ShouldNotReturnAllRestaurants()
        {
            // Arrange
            var restaurants = _fixture.CreateMany<Restaurant>().ToList();
            var emptyList = new List<Restaurant>();
            _mockRestaurantRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyList);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _restaurantService.GetAllRestaurantsAsync();
            var output = stringWriter.ToString().Trim();
            Console.SetOut(Console.Out);
            // Assert
            _mockRestaurantRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.Contains("No restaurants found", output);
        }

        [Fact]
        public async Task GetRestaurantByIdAsync_ShouldReturnRestaurant()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurant.RestaurantId)).ReturnsAsync(restaurant);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _restaurantService.GetRestaurantByIdAsync(restaurant.RestaurantId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _mockRestaurantRepository.Verify(repo => repo.GetByIdAsync(restaurant.RestaurantId), Times.Once);
            Assert.Contains(restaurant.ToString(), output);
        }

        [Fact]
        public async Task GetRestaurantByIdAsync_ShouldNotReturnRestaurant()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            Restaurant emptyRestaurant = null;
            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurant.RestaurantId)).ReturnsAsync(emptyRestaurant);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _restaurantService.GetRestaurantByIdAsync(restaurant.RestaurantId);
            var output = stringWriter.ToString().Trim();
            Console.SetOut(Console.Out);

            // Assert
            _mockRestaurantRepository.Verify(repo => repo.GetByIdAsync(restaurant.RestaurantId), Times.Once);
            Assert.Contains("Restaurant doesn't exist", output);
        }

        [Fact]
        public async Task UpdateRestaurantAsync_ShouldUpdateRestaurant_WhenRestaurantExists()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurant.RestaurantId)).ReturnsAsync(restaurant);

            // Act
            await _restaurantService.UpdateRestaurantAsync(restaurant.RestaurantId, restaurant);

            // Assert
            _mockRestaurantRepository.Verify(repo => repo.UpdateAsync(restaurant), Times.Once);
        }

        [Fact]
        public async Task UpdateRestaurantAsync_ShouldNotUpdateRestaurant_WhenRestaurantDoesNotExist()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurant.RestaurantId)).ReturnsAsync((Restaurant)null);

            // Act
            await _restaurantService.UpdateRestaurantAsync(restaurant.RestaurantId, restaurant);

            // Assert
            _mockRestaurantRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Restaurant>()), Times.Never);
        }

        [Fact]
        public async Task DeleteRestaurantAsync_ShouldDeleteRestaurant_WhenRestaurantExists()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurant.RestaurantId)).ReturnsAsync(restaurant);

            // Act
            await _restaurantService.DeleteRestaurantAsync(restaurant.RestaurantId);

            // Assert
            _mockRestaurantRepository.Verify(repo => repo.DeleteAsync(restaurant), Times.Once);
        }

        [Fact]
        public async Task DeleteRestaurantAsync_ShouldNotDeleteRestaurant_WhenRestaurantDoesNotExist()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurant.RestaurantId)).ReturnsAsync((Restaurant)null);

            // Act
            await _restaurantService.DeleteRestaurantAsync(restaurant.RestaurantId);

            // Assert
            _mockRestaurantRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Restaurant>()), Times.Never);
        }
    }
}
