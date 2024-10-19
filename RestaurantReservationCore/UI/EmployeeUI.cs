using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.UI
{
    public class EmployeeUI
    {
        private readonly EmployeeService _employeeService;

        public EmployeeUI(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task DisplayOptionsAsync()
        {
            while (true)
            {
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Update Employee");
                Console.WriteLine("3. Delete Employee");
                Console.WriteLine("4. View All Employees");
                Console.WriteLine("5. View Employee By Id");
                Console.WriteLine("6. View All Managers");
                Console.WriteLine("7. Employee Average Order Amount");
                Console.WriteLine("0. Go Back");
                string input = Console.ReadLine();
                try
                {
                    await ProcessInputAsync(input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task ProcessInputAsync(string input)
        {
            if (await TryHandleOperationAsync(input))
            {
                return;
            }

            await TryHandleEmployeeOptionAsync(input);
        }

        private async Task<bool> TryHandleOperationAsync(string input)
        {
            if (Enum.TryParse(input, out OperationOptions option) && Enum.IsDefined(typeof(OperationOptions), option))
            {
                if (option == OperationOptions.Exit)
                {
                    return true;
                }

                await HandleRequestAsync(option);
                return true;
            }
            return false;
        }

        private async Task TryHandleEmployeeOptionAsync(string input)
        {
            if (Enum.TryParse(input, out EmployeeOptions employeeOption) && Enum.IsDefined(typeof(EmployeeOptions), employeeOption))
            {
                switch (employeeOption)
                {
                    case EmployeeOptions.ViewAllManagers:
                        await ListAllManagersAsync();
                        break;

                    case EmployeeOptions.ViewEmployeeAverageAmount:
                        await EmployeeAverageAmountAsync();
                        break;
                }
            }
        }

        public async Task HandleRequestAsync(OperationOptions option)
        {
            switch (option)
            {
                case OperationOptions.Add:
                    await AddEmployeeAsync();
                    break;

                case OperationOptions.Update:
                    await UpdateEmployeeAsync();
                    break;

                case OperationOptions.Delete:
                    await DeleteEmployeeAsync();
                    break;

                case OperationOptions.View:
                    await ViewAllEmployeesAsync();
                    break;

                case OperationOptions.Search:
                    await ViewEmployeeByIdAsync();
                    break;
            }
        }

        private async Task AddEmployeeAsync()
        {
            Console.WriteLine("Enter First Name");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter Position");
            string position = Console.ReadLine();
            Console.WriteLine("Enter Restaurant ID");
            int restaurantId = Convert.ToInt32(Console.ReadLine());
            Employee employee = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                Position = position,
                RestaurantId = restaurantId
            };
            await _employeeService.AddEmployeeAsync(employee);
        }

        private async Task UpdateEmployeeAsync()
        {
            Console.WriteLine("Enter employee id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter First Name");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter Position");
            string position = Console.ReadLine();
            Console.WriteLine("Enter Restaurant id");
            int restaurantId = Convert.ToInt32(Console.ReadLine());
            Employee employee = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                Position = position,
                RestaurantId = restaurantId
            };
            await _employeeService.UpdateEmployeeAsync(id, employee);
        }

        private async Task ViewEmployeeByIdAsync()
        {
            Console.WriteLine("Enter employee id: ");
            int id = int.Parse(Console.ReadLine());
            await _employeeService.GetEmployeeByIdAsync(id);
        }

        private async Task ViewAllEmployeesAsync()
        {
            await _employeeService.GetAllEmployeesAsync();
        }

        private async Task DeleteEmployeeAsync()
        {
            Console.WriteLine("Enter employee id: ");
            int id = int.Parse(Console.ReadLine());
            await _employeeService.DeleteEmployeeAsync(id);
        }

        private async Task ListAllManagersAsync()
        {
            await _employeeService.GetAllManagersAsync();
        }

        private async Task EmployeeAverageAmountAsync()
        {
            Console.WriteLine("Enter employee id: ");
            int id = int.Parse(Console.ReadLine());
            await _employeeService.CalculateAverageOrderAmountAsync(id);
        }
    }
}