using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.EmployeeManagement;

namespace RestaurantReservationCore.Tests.EmployeeTests
{
    [Trait("Category", "RepositoryTests")]
    public class EmployeeRepositoryTests
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly EmployeeRepository _employeeRepository;
        private readonly IFixture _fixture;

        public EmployeeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantReservationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantReservationDbContext(options);

            _employeeRepository = new EmployeeRepository(_context);

            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldReturnAllEmployees()
        {
            // Arrange
            var employees = _fixture.CreateMany<Employee>(10).ToList();
            await _context.Employees.AddRangeAsync(employees);
            await _context.SaveChangesAsync();

            // Act
            var result = await _employeeRepository.GetAllAsync();

            // Assert
            Assert.Equal(employees.Count, result.Count);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldReturnEmployee()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            // Act
            var result = await _employeeRepository.GetByIdAsync(employee.EmployeeId);

            // Assert
            Assert.Equal(employee.EmployeeId, result.EmployeeId);
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldAddEmployee()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();

            // Act
            await _employeeRepository.AddAsync(employee);

            // Assert
            var result = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
            Assert.Equal(employee.EmployeeId, result.EmployeeId);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldUpdateEmployee()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            // Act
            employee.FirstName = "Updated";
            await _employeeRepository.UpdateAsync(employee);

            // Assert
            var result = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
            Assert.Equal("Updated", result.FirstName);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ShouldDeleteEmployee()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            // Act
            await _employeeRepository.DeleteAsync(employee);

            // Assert
            var result = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
            Assert.Null(result);
        }
    }
}