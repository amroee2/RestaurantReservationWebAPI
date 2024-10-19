using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.ReservationManagement;

namespace RestaurantReservationCore.Tests.ReservationTests
{
    [Trait("Category", "RepositoryTests")]
    public class ReservationRepositoryTests
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly ReservationRepository reservationRepository;
        private readonly IFixture _fixture;

        public ReservationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantReservationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantReservationDbContext(options);

            reservationRepository = new ReservationRepository(_context);

            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllReservationsAsync_ShouldReturnAllReservations()
        {
            // Arrange
            var reservations = _fixture.CreateMany<Reservation>(10).ToList();
            await _context.Reservations.AddRangeAsync(reservations);
            await _context.SaveChangesAsync();

            // Act
            var result = await reservationRepository.GetAllAsync();

            // Assert
            Assert.Equal(reservations.Count, result.Count);
        }

        [Fact]
        public async Task GetReservationByIdAsync_ShouldReturnReservation()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            // Act
            var result = await reservationRepository.GetByIdAsync(reservation.ReservationId);

            // Assert
            Assert.Equal(reservation.ReservationId, result.ReservationId);
        }

        [Fact]
        public async Task AddReservationAsync_ShouldAddReservation()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();

            // Act
            await reservationRepository.AddAsync(reservation);

            // Assert
            var result = await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservation.ReservationId);
            Assert.Equal(reservation.ReservationId, result.ReservationId);
        }

        [Fact]
        public async Task UpdateReservationAsync_ShouldUpdateReservation()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            // Act
            reservation.ReservationDate = DateTime.Now;
            await reservationRepository.UpdateAsync(reservation);

            // Assert
            var result = await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservation.ReservationId);
            Assert.Equal(reservation.ReservationDate, result.ReservationDate);
        }

        [Fact]
        public async Task DeleteReservationAsync_ShouldDeleteReservation()
        {
            // Arrange
            var reservation = _fixture.Create<Reservation>();
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            // Act
            await reservationRepository.DeleteAsync(reservation);

            // Assert
            var result = await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservation.ReservationId);
            Assert.Null(result);
        }
    }
}