using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.EmployeeManagement;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.Tests.EmployeeTests
{
    [Trait("Category", "ServiceTests")]
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly EmployeeService _employeeService;
        private readonly IFixture _fixture;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object);
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldAddEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employee.EmployeeId)).ReturnsAsync((Employee)null);

            // Act
            await _employeeService.AddEmployeeAsync(employee);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.AddAsync(employee), Times.Once);
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldNotAddEmployee_WhenEmployeeAlreadyExists()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employee.EmployeeId)).ReturnsAsync(employee);

            // Act
            await _employeeService.AddEmployeeAsync(employee);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Employee>()), Times.Never);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldReturnAllEmployees()
        {
            // Arrange
            var employees = _fixture.CreateMany<Employee>().ToList();
            _employeeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employees);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _employeeService.GetAllEmployeesAsync();
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            foreach (var employee in employees)
            {
                Assert.Contains(employee.ToString(), output);
            }
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldNotReturnAllEmployees()
        {
            // Arrange
            var employees = _fixture.CreateMany<Employee>().ToList();
            var emptyList = new List<Employee>();
            _employeeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyList);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _employeeService.GetAllEmployeesAsync();
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.Contains("No employees found", output);
        }
        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employee.EmployeeId)).ReturnsAsync(employee);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _employeeService.GetEmployeeByIdAsync(employee.EmployeeId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.GetByIdAsync(employee.EmployeeId), Times.Once);
            Assert.Contains(employee.ToString(), output);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldNotReturnEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employee.EmployeeId)).ReturnsAsync((Employee)null);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _employeeService.GetEmployeeByIdAsync(employee.EmployeeId);
            var output = stringWriter.ToString();
            Console.SetOut(Console.Out);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.GetByIdAsync(employee.EmployeeId), Times.Once);
            Assert.Contains("Employee not found", output);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldUpdateEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employee.EmployeeId)).ReturnsAsync(employee);

            // Act
            await _employeeService.UpdateEmployeeAsync(employee.EmployeeId, employee);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.UpdateAsync(employee), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldNotUpdateEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employee.EmployeeId)).ReturnsAsync((Employee)null);

            // Act
            await _employeeService.UpdateEmployeeAsync(employee.EmployeeId, employee);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Employee>()), Times.Never);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ShouldDeleteEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employee.EmployeeId)).ReturnsAsync(employee);

            // Act
            await _employeeService.DeleteEmployeeAsync(employee.EmployeeId);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.DeleteAsync(employee), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ShouldNotDeleteEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employee = _fixture.Create<Employee>();
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employee.EmployeeId)).ReturnsAsync((Employee)null);

            // Act
            await _employeeService.DeleteEmployeeAsync(employee.EmployeeId);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Employee>()), Times.Never);
        }
    }
}