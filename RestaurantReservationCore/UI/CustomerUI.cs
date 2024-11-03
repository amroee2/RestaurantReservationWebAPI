using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.UI
{
    public class CustomerUI
    {
        private readonly CustomerService _customerService;

        public CustomerUI(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task DisplayOptionsAsync()
        {
            while (true)
            {
                const int customersWithBigPartySize = 6;
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Update Customer");
                Console.WriteLine("3. Delete Customer");
                Console.WriteLine("4. View All Customers");
                Console.WriteLine("5. View Customer By Id");
                Console.WriteLine("6. Customers With Big Party Size");
                Console.WriteLine("0. Go Back");

                string input = Console.ReadLine();
                if (Convert.ToInt32(input) == customersWithBigPartySize)
                {
                    await GetCustomersWithBigPartySizeAsync();
                }

                try
                {
                    Enum.TryParse(input, out OperationOptions option);
                    if (option == OperationOptions.Exit)
                    {
                        return;
                    }
                    await HandleRequestAsync(option);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task HandleRequestAsync(OperationOptions option)
        {
            switch (option)
            {
                case OperationOptions.Add:
                    await AddCustomerAsync();
                    break;

                case OperationOptions.Update:
                    await UpdateCustomerAsync();
                    break;

                case OperationOptions.Delete:
                    await DeleteCustomerAsync();
                    break;

                case OperationOptions.View:
                    await ViewAllCustomersAsync();
                    break;

                case OperationOptions.Search:
                    await ViewCustomerByIdAsync();
                    break;
            }
        }

        private async Task ViewCustomerByIdAsync()
        {
            Console.WriteLine("Enter customer id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            await _customerService.GetCustomerByIdAsync(id);
        }

        private async Task ViewAllCustomersAsync()
        {
            await _customerService.GetAllCustomersAsync();
        }

        private async Task DeleteCustomerAsync()
        {
            Console.WriteLine("Enter customer id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            await _customerService.DeleteCustomerAsync(id);
        }

        private async Task UpdateCustomerAsync()
        {
            Console.WriteLine("Enter customer id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter customer first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter customer last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter customer email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter customer phone number: ");
            string phoneNumber = Console.ReadLine();
            Customer customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };
            await _customerService.UpdateCustomerAsync(id, customer);
        }

        private async Task AddCustomerAsync()
        {
            Console.WriteLine("Enter customer information\n");
            Console.WriteLine("Enter customer first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter customer last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter customer email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter customer phone number: ");
            string phoneNumber = Console.ReadLine();
            Customer customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };
            await _customerService.AddCustomerAsync(customer);
        }

        private async Task GetCustomersWithBigPartySizeAsync()
        {
            Console.WriteLine("Enter Party Size");
            int partySize = Convert.ToInt32(Console.ReadLine());
            await _customerService.GetCustomersWithBigPartySizeAsync(partySize);
        }
    }
}