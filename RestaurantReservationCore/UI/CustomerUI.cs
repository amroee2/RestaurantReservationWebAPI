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

        public void DisplayOptions()
        {
            while (true)
            {
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Update Customer");
                Console.WriteLine("3. Delete Customer");
                Console.WriteLine("4. View All Customers");
                Console.WriteLine("5. View Customer By Id");
                Console.WriteLine("0. Go Back");

                try
                {
                    Enum.TryParse(Console.ReadLine(), out OperationOptions option);
                    if (option == OperationOptions.Exit)
                    {
                        return;
                    }
                    HandleRequest(option);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void HandleRequest(OperationOptions option)
        {
            switch (option)
            {
                case OperationOptions.Add:
                    AddCustomer();
                    break;
                case OperationOptions.Update:
                    UpdateCustomer();
                    break;
                case OperationOptions.Delete:
                    DeleteCustomer();
                    break;
                case OperationOptions.View:
                    ViewAllCustomers();
                    break;
                case OperationOptions.Search:
                    ViewCustomerById();
                    break;
            }
        }

        private void ViewCustomerById()
        {
            Console.WriteLine("Enter customer id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            _customerService.GetCustomerByIdAsync(id).Wait();
        }

        private void ViewAllCustomers()
        {
            _customerService.GetAllCustomersAsync().Wait();
        }

        private void DeleteCustomer()
        {
            Console.WriteLine("Enter customer id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            _customerService.DeleteCustomerAsync(id).Wait();
        }

        private void UpdateCustomer()
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
            _ = _customerService.UpdateCustomerAsync(id, customer);
        }

        private void AddCustomer()
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
            _ = _customerService.AddCustomerAsync(customer);


        }

    }
}
