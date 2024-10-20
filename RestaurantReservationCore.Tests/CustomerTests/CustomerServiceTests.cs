using AutoFixture;
using Moq;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.CustomerManagement;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.Tests.CustomerTests
{
    [Trait("Category", "ServiceTests")]
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldAddCustomer_WhenCustomerDoesNotExist()
        {
            // Arrange
            var newCustomer = new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" };
            Customer mockCustomer = null;
            _customerRepositoryMock
                .Setup(repo => repo.GetByIdAsync(newCustomer.CustomerId)).ReturnsAsync(mockCustomer);
            // Act
            await _customerService.AddCustomerAsync(newCustomer);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.AddAsync(newCustomer), Times.Once);
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldNotAddCustomer_WhenCustomerAlreadyExists()
        {
            // Arrange
            var existingCustomer = new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" };
            _customerRepositoryMock
                .Setup(repo => repo.GetByIdAsync(existingCustomer.CustomerId))
                .ReturnsAsync(existingCustomer);

            // Act
            await _customerService.AddCustomerAsync(existingCustomer);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            Fixture fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var customers = fixture.CreateMany<Customer>(2).ToList();
            _customerRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Act
            await _customerService.GetAllCustomersAsync();
            var output = stringWriter.ToString().Trim().Split(Environment.NewLine);
            Console.SetOut(Console.Out);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            var expectedOutput = string.Join(Environment.NewLine, customers.Select(c => c.ToString()));
            foreach (var customer in customers)
            {
                Assert.Contains(customer.ToString(), output);
            }
        }

        [Fact]
        public async Task GetAllCustomersAsync_ShouldNotReturnAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Doe" }
            };
            var emptyList = new List<Customer>();
            _customerRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyList);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Act
            await _customerService.GetAllCustomersAsync();
            var output = stringWriter.ToString().Trim().Split(Environment.NewLine);
            Console.SetOut(Console.Out);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.Contains("No customers found", output);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnCustomer()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "059" };
            _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(customer);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _customerService.GetCustomerByIdAsync(customer.CustomerId);
            var output = stringWriter.ToString().Trim();
            Console.SetOut(Console.Out);
            // Assert
            _customerRepositoryMock.Verify(repo => repo.GetByIdAsync(customer.CustomerId), Times.Once);
            Assert.Equal(customer.ToString(), string.Join(",", output));
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldNotReturnCustomer()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" };
            Customer mockCustomer = null;
            _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(mockCustomer);

            // Act
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await _customerService.GetCustomerByIdAsync(customer.CustomerId);
            var output = stringWriter.ToString().Trim().Split(Environment.NewLine);
            Console.SetOut(Console.Out);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.GetByIdAsync(customer.CustomerId), Times.Once);
            Assert.Contains("Customer not found", output);
        }

        [Fact]
        public async Task UpdateCustomerAsync_ShouldUpdateCustomer()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" };
            var updatedCustomer = new Customer { CustomerId = 1, FirstName = "Jane", LastName = "Doe" };
            _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(customer);

            // Act
            await _customerService.UpdateCustomerAsync(customer.CustomerId, updatedCustomer);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Customer>(c =>
                c.CustomerId == updatedCustomer.CustomerId &&
                c.FirstName == updatedCustomer.FirstName &&
                c.LastName == updatedCustomer.LastName)), Times.Once);
        }

        [Fact]
        public async Task UpdateCustomerAsync_ShouldNotUpdateCustomer()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" };
            var updatedCustomer = new Customer { CustomerId = 1, FirstName = "Jane", LastName = "Doe" };
            Customer mockCustomer = null;
            _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(mockCustomer);

            // Act
            await _customerService.UpdateCustomerAsync(customer.CustomerId, updatedCustomer);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.UpdateAsync(updatedCustomer), Times.Never);
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldDeleteCustomer()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" };
            _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(customer);

            // Act
            await _customerService.DeleteCustomerAsync(customer.CustomerId);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.DeleteAsync(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldNotDeleteCustomer()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" };
            Customer mockCustomer = null;
            _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(mockCustomer);

            // Act
            await _customerService.DeleteCustomerAsync(customer.CustomerId);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.DeleteAsync(customer), Times.Never);
        }
    }
}