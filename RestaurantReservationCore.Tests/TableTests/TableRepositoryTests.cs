using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;

namespace RestaurantReservationCore.Tests.TableTests
{
    [Trait("Category", "RepositoryTests")]
    public class TableRepositoryTests
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly TableRepository _tableRepository;
        private readonly IFixture _fixture;

        public TableRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantReservationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantReservationDbContext(options);

            _tableRepository = new TableRepository(_context);

            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllTablesAsync_ShouldReturnAllTables()
        {
            // Arrange
            var tables = _fixture.CreateMany<Table>(10).ToList();
            await _context.Tables.AddRangeAsync(tables);
            await _context.SaveChangesAsync();

            // Act
            var result = await _tableRepository.GetAllAsync();

            // Assert
            Assert.Equal(tables.Count, result.Count);
        }

        [Fact]
        public async Task GetTableByIdAsync_ShouldReturnTable()
        {
            // Arrange
            var table = _fixture.Create<Table>();
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();

            // Act
            var result = await _tableRepository.GetByIdAsync(table.TableId);

            // Assert
            Assert.Equal(table.TableId, result.TableId);
        }

        [Fact]
        public async Task AddTableAsync_ShouldAddTable()
        {
            // Arrange
            var table = _fixture.Create<Table>();

            // Act
            await _tableRepository.AddAsync(table);

            // Assert
            var result = await _context.Tables.FindAsync(table.TableId);
            Assert.Equal(table.TableId, result.TableId);
        }

        [Fact]
        public async Task UpdateTableAsync_ShouldUpdateTable()
        {
            // Arrange
            var table = _fixture.Create<Table>();
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();

            table.Capacity = 10;

            // Act
            await _tableRepository.UpdateAsync(table);

            // Assert
            Assert.Equal(10, table.Capacity);
        }

        [Fact]
        public async Task DeleteTableAsync_ShouldDeleteTable()
        {
            // Arrange
            var table = _fixture.Create<Table>();
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();

            // Act
            await _tableRepository.DeleteAsync(table);

            // Assert
            var result = await _context.Tables.FindAsync(table.TableId);
            Assert.Null(result);
        }
    }
}