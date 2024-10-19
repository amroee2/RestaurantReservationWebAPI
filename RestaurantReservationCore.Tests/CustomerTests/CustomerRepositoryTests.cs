using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.CustomerManagement;

namespace RestaurantReservationCore.Tests.CustomerTests
{
    [Trait("Category", "RepositoryTests")]
    public class CustomerRepositoryTests
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly CustomerRepository _customerRepository;
        private readonly IFixture _fixture;

        public CustomerRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestaurantReservationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RestaurantReservationDbContext(options);

            _customerRepository = new CustomerRepository(_context);

            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllCustomersAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var customers = _fixture.CreateMany<Customer>(10).ToList();
            await _context.Customers.AddRangeAsync(customers);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerRepository.GetAllAsync();

            // Assert
            Assert.Equal(customers.Count, result.Count);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnCustomer()
        {
            // Arrange
            var customer = _fixture.Create<Customer>();
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerRepository.GetByIdAsync(customer.CustomerId);

            // Assert
            Assert.Equal(customer.CustomerId, result.CustomerId);
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldAddCustomer()
        {
            // Arrange
            var customer = _fixture.Create<Customer>();

            // Act
            await _customerRepository.AddAsync(customer);

            // Assert
            var result = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCustomerAsync_ShouldUpdateCustomer()
        {
            // Arrange
            var customer = _fixture.Create<Customer>();
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            // Act
            customer.FirstName = "Updated";
            await _customerRepository.UpdateAsync(customer);

            // Assert
            var result = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
            Assert.Equal("Updated", result.FirstName);
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldDeleteCustomer()
        {
            // Arrange
            var customer = _fixture.Create<Customer>();
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            // Act
            await _customerRepository.DeleteAsync(customer);

            // Assert
            var result = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
            Assert.Null(result);
        }
    }
}