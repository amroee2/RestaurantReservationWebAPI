using AutoFixture;
using Moq;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.ReservationManagement;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.Tests.ReservationTests
{
    [Trait("Category", "ServiceTests")]
    public class ReservationServiceTests
    {
        private readonly Mock<IReservationRepository> _mockReservationRepository;
        private readonly ReservationService _reservationService;
        private readonly IFixture _fixture;
        public ReservationServiceTests()
        {
            _fixture = new Fixture();
            _mockReservationRepository = new Mock<IReservationRepository>();
            _reservationService = new ReservationService(_mockReservationRepository.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                                .ToList()
                                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task AddReservationAsync_ShouldAddReservation_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            _mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservation.ReservationId)).ReturnsAsync((Reservation)null);

            // Act
            await _reservationService.AddReservationAsync(reservation);

            // Assert
            _mockReservationRepository.Verify(repo => repo.AddAsync(reservation), Times.Once);
        }

        [Fact]
        public async Task AddReservationAsync_ShouldNotAddReservation_WhenReservationAlreadyExists()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            _mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservation.ReservationId)).ReturnsAsync(reservation);

            // Act
            await _reservationService.AddReservationAsync(reservation);

            // Assert
            _mockReservationRepository.Verify(repo => repo.AddAsync(It.IsAny<Reservation>()), Times.Never);
        }

        [Fact]
        public async Task GetAllReservationsAsync_ShouldReturnAllReservations()
        {
            // Arrange
            var reservations = _fixture.CreateMany<Reservation>(10).ToList();
            _mockReservationRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(reservations);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _reservationService.GetAllReservationsAsync();
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);
            // Assert
            _mockReservationRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            foreach(var reservation in reservations)
            {
                Assert.Contains(reservation.ReservationId.ToString(), output);
            }
        }

        [Fact]
        public async Task GetAllReservationsAsync_ShouldNotReturnAllReservations()
        {
            // Arrange
            var reservations = _fixture.CreateMany<Reservation>(10).ToList();
            var emptyList = new List<Reservation>();
            _mockReservationRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyList);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _reservationService.GetAllReservationsAsync();
            var output = stringWriter.ToString().Trim();
            Console.SetOut(Console.Out);
            // Assert
            _mockReservationRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.Equal("No reservations found", output);
        }

        [Fact]
        public async Task GetReservationByIdAsync_ShouldReturnReservation()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            _mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservation.ReservationId)).ReturnsAsync(reservation);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _reservationService.GetReservationByIdAsync(reservation.ReservationId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _mockReservationRepository.Verify(repo => repo.GetByIdAsync(reservation.ReservationId), Times.Once);
            Assert.Contains(reservation.ToString(), output);
        }

        [Fact]
        public async Task GetReservationByIdAsync_ShouldNotReturnReservation()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            _mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservation.ReservationId)).ReturnsAsync((Reservation)null);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _reservationService.GetReservationByIdAsync(reservation.ReservationId);
            var output = stringWriter.ToString().Trim();
            Console.SetOut(Console.Out);

            // Assert
            _mockReservationRepository.Verify(repo => repo.GetByIdAsync(reservation.ReservationId), Times.Once);
            Assert.Contains("Reservation not found", output);
        }

        [Fact]
        public async Task UpdateReservationAsync_ShouldUpdateReservation_WhenReservationExists()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            _mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservation.ReservationId)).ReturnsAsync(reservation);

            // Act
            await _reservationService.UpdateReservationAsync(reservation.ReservationId, reservation);

            // Assert
            _mockReservationRepository.Verify(repo => repo.UpdateAsync(reservation), Times.Once);
        }

        [Fact]
        public async Task UpdateReservationAsync_ShouldNotUpdateReservation_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            _mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservation.ReservationId)).ReturnsAsync((Reservation)null);

            // Act
            await _reservationService.UpdateReservationAsync(reservation.ReservationId, reservation);

            // Assert
            _mockReservationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Reservation>()), Times.Never);
        }

        [Fact]
        public async Task DeleteReservationAsync_ShouldDeleteReservation_WhenReservationExists()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            _mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservation.ReservationId)).ReturnsAsync(reservation);

            // Act
            await _reservationService.DeleteReservationAsync(reservation.ReservationId);

            // Assert
            _mockReservationRepository.Verify(repo => repo.DeleteAsync(reservation), Times.Once);
        }

        [Fact]
        public async Task DeleteReservationAsync_ShouldNotDeleteReservation_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            _mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservation.ReservationId)).ReturnsAsync((Reservation)null);

            // Act
            await _reservationService.DeleteReservationAsync(reservation.ReservationId);

            // Assert
            _mockReservationRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Reservation>()), Times.Never);
        }

        [Fact]
        public async Task GetReservationsByCustomerIdAsync_ShouldReturnReservations()
        {
            // Arrange
            var customerId = _fixture.Create<int>();
            var reservations = _fixture.Build<Reservation>()
                .With(r => r.CustomerId, customerId)
                .Without(r => r.Customer)
                .CreateMany(5)
                .ToList();
            _mockReservationRepository.Setup(repo => repo.GetReservationsByCustomerIdAsync(customerId)).ReturnsAsync(reservations);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _reservationService.GetReservationsByCustomerIdAsync(customerId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _mockReservationRepository.Verify(repo => repo.GetReservationsByCustomerIdAsync(customerId), Times.Once);
            foreach (var reservation in reservations)
            {
                Assert.Contains(reservation.ToString(), output);
            }
        }

        [Fact]
        public async Task GetReservationsByCustomerIdAsync_ShouldNotReturnReservations()
        {
            // Arrange
            var customerId = _fixture.Create<int>();
            var reservations = _fixture.CreateMany<Reservation>(10).ToList();
            var emptyList = new List<Reservation>();
            _mockReservationRepository.Setup(repo => repo.GetReservationsByCustomerIdAsync(customerId)).ReturnsAsync(emptyList);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _reservationService.GetReservationsByCustomerIdAsync(customerId);
            var output = stringWriter.ToString().Trim();
            Console.SetOut(Console.Out);

            // Assert
            _mockReservationRepository.Verify(repo => repo.GetReservationsByCustomerIdAsync(customerId), Times.Once);
            Assert.Equal("No reservations found", output);
        }
    }
}
