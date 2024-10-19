using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;

namespace RestaurantReservationCore.Services
{
    public class CustomerService
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task GetAllCustomersAsync()
        {
            List<Customer> customers =  await _customerRepository.GetAllAsync();
            if(!customers.Any())
            {
                Console.WriteLine("No customers found");
                return;
            }
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        public async Task GetCustomerByIdAsync(int id)
        {
            Customer customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }
            Console.WriteLine(customer);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            Customer requestedCustomer = await _customerRepository.GetByIdAsync(customer.CustomerId);
            if (requestedCustomer != null)
            {
                Console.WriteLine("Customer already exists");
                return;
            }
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(int Id, Customer customer)
        {
            Customer updatedCustomer = await _customerRepository.GetByIdAsync(Id);
            if (updatedCustomer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }
            updatedCustomer.PhoneNumber = customer.PhoneNumber;
            updatedCustomer.Email = customer.Email;
            updatedCustomer.FirstName = customer.FirstName;
            updatedCustomer.LastName = customer.LastName;
            await _customerRepository.UpdateAsync(updatedCustomer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            Customer customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }
            await _customerRepository.DeleteAsync(customer);
        }

        public async Task GetCustomersWithBigPartySizeAsync(int partySize)
        {
            var customers = await _customerRepository.GetCustomersWithBigPartySizeAsync(partySize);
            if (!customers.Any())
            {
                Console.WriteLine("No customers found");
                return;
            }
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }
    }
}
