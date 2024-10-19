using AutoFixture;
using Moq;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.Tests.TableTests
{
    [Trait("Category", "ServiceTests")]
    public class TableServiceTests
    {
        private readonly Mock<IRepository<Table>> _mockTableRepository;
        private readonly TableService _tableService;
        private readonly IFixture fixture;
        public TableServiceTests()
        {
            _mockTableRepository = new Mock<IRepository<Table>>();
            _tableService = new TableService(_mockTableRepository.Object);
            fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task AddTableAsync_ShouldAddTable_WhenTableDoesNotExist()
        {
            // Arrange
            var table = fixture.Create<Table>();
            _mockTableRepository.Setup(repo => repo.GetByIdAsync(table.TableId)).ReturnsAsync((Table)null);

            // Act
            await _tableService.AddTableAsync(table);

            // Assert
            _mockTableRepository.Verify(repo => repo.AddAsync(table), Times.Once);
        }

        [Fact]
        public async Task AddTableAsync_ShouldNotAddTable_WhenTableAlreadyExists()
        {
            // Arrange
            var table = fixture.Create<Table>();
            _mockTableRepository.Setup(repo => repo.GetByIdAsync(table.TableId)).ReturnsAsync(table);

            // Act
            await _tableService.AddTableAsync(table);

            // Assert
            _mockTableRepository.Verify(repo => repo.AddAsync(It.IsAny<Table>()), Times.Never);
        }

        [Fact]
        public async Task GetAllTablesAsync_ShouldReturnAllTables()
        {
            // Arrange
            var tables = fixture.CreateMany<Table>().ToList();
            _mockTableRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tables);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _tableService.GetAllTablesAsync();
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);
            // Assert
            _mockTableRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            foreach (var table in tables)
            {
                Assert.Contains(table.ToString(), output);
            }
        }

        [Fact]
        public async Task GetAllTablesAsync_ShouldNotReturnAllTables()
        {
            // Arrange
            var tables = fixture.CreateMany<Table>().ToList();
            var emptyTables = new List<Table>();
            _mockTableRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyTables);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _tableService.GetAllTablesAsync();
            var output = stringWriter.ToString().Trim();
            Console.SetOut(Console.Out);
            // Assert
            _mockTableRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.Equal("There are currently no tables", output);
        }

        [Fact]
        public async Task GetTableByIdAsync_ShouldReturnTable_WhenTableExists()
        {
            // Arrange
            var table = fixture.Create<Table>();
            _mockTableRepository.Setup(repo => repo.GetByIdAsync(table.TableId)).ReturnsAsync(table);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _tableService.GetTableByIdAsync(table.TableId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);
            // Assert
            _mockTableRepository.Verify(repo => repo.GetByIdAsync(table.TableId), Times.Once);
            Assert.Contains(table.ToString(), output);
        }

        [Fact]
        public async Task GetTableByIdAsync_ShouldNotReturnTable_WhenTableDoesNotExist()
        {
            // Arrange
            var table = fixture.Create<Table>();
            _mockTableRepository.Setup(repo => repo.GetByIdAsync(table.TableId)).ReturnsAsync((Table)null);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _tableService.GetTableByIdAsync(table.TableId);
            var output = stringWriter.ToString().Trim();
            Console.SetOut(Console.Out);
            // Assert
            _mockTableRepository.Verify(repo => repo.GetByIdAsync(table.TableId), Times.Once);
            Assert.Equal("Table doesn't exist", output);
        }

        [Fact]
        public async Task UpdateTableAsync_ShouldUpdateTable_WhenTableExists()
        {
            // Arrange
            var table = fixture.Create<Table>();
            _mockTableRepository.Setup(repo => repo.GetByIdAsync(table.TableId)).ReturnsAsync(table);

            // Act
            await _tableService.UpdateTableAsync(table.TableId, table);

            // Assert
            _mockTableRepository.Verify(repo => repo.UpdateAsync(table), Times.Once);
        }

        [Fact]
        public async Task UpdateTableAsync_ShouldNotUpdateTable_WhenTableDoesNotExist()
        {
            // Arrange
            var table = fixture.Create<Table>();
            _mockTableRepository.Setup(repo => repo.GetByIdAsync(table.TableId)).ReturnsAsync((Table)null);

            // Act
            await _tableService.UpdateTableAsync(table.TableId, table);

            // Assert
            _mockTableRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Table>()), Times.Never);
        }

        [Fact]
        public async Task DeleteTableAsync_ShouldDeleteTable_WhenTableExists()
        {
            // Arrange
            var table = fixture.Create<Table>();
            _mockTableRepository.Setup(repo => repo.GetByIdAsync(table.TableId)).ReturnsAsync(table);

            // Act
            await _tableService.DeleteTableAsync(table.TableId);

            // Assert
            _mockTableRepository.Verify(repo => repo.DeleteAsync(table), Times.Once);
        }

        [Fact]
        public async Task DeleteTableAsync_ShouldNotDeleteTable_WhenTableDoesNotExist()
        {
            // Arrange
            var table = fixture.Create<Table>();
            _mockTableRepository.Setup(repo => repo.GetByIdAsync(table.TableId)).ReturnsAsync((Table)null);

            // Act
            await _tableService.DeleteTableAsync(table.TableId);

            // Assert
            _mockTableRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Table>()), Times.Never);
        }
    }
}
