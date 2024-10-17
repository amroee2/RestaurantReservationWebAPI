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

        public void DisplayOptions()
        {
            while (true)
            {
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Update Employee");
                Console.WriteLine("3. Delete Employee");
                Console.WriteLine("4. View All Employees");
                Console.WriteLine("5. View Employee By Id");
                Console.WriteLine("6. View All Managers");
                Console.WriteLine("7. Employee Total Amount");
                Console.WriteLine("0. Go Back");
                string input = Console.ReadLine();
                try
                {
                    ProcessInput(input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void ProcessInput(string input)
        {
            if (TryHandleOperation(input))
            {
                return;
            }

            TryHandleEmployeeOption(input);
        }

        private bool TryHandleOperation(string input)
        {
            if (Enum.TryParse(input, out OperationOptions option) && Enum.IsDefined(typeof(OperationOptions), option))
            {
                if (option == OperationOptions.Exit)
                {
                    return true;
                }

                HandleRequest(option);
                return true;
            }
            return false;
        }

        private void TryHandleEmployeeOption(string input)
        {
            if (Enum.TryParse(input, out EmployeeOptions employeeOption) && Enum.IsDefined(typeof(EmployeeOptions), employeeOption))
            {
                switch (employeeOption)
                {
                    case EmployeeOptions.ViewAllManagers:
                        ListAllManagers();
                        break;

                    case EmployeeOptions.ViewEmployeeAverageAmount:
                        EmployeeAverageAmount();
                        break;
                }
            }
        }
        public void HandleRequest(OperationOptions option)
        {
            switch (option)
            {
                case OperationOptions.Add:
                    AddEmployee();
                    break;
                case OperationOptions.Update:
                    UpdateEmployee();
                    break;
                case OperationOptions.Delete:
                    DeleteEmployee();
                    break;
                case OperationOptions.View:
                    ViewAllEmployees();
                    break;
                case OperationOptions.Search:
                    ViewEmployeeById();
                    break;
            }
        }

        private void AddEmployee()
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
            _employeeService.AddEmployeeAsync(employee);
        }

        private void UpdateEmployee()
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
            _employeeService.UpdateEmployeeAsync(id, employee);
        }

        private void ViewEmployeeById()
        {
            Console.WriteLine("Enter employee id: ");
            int id = int.Parse(Console.ReadLine());
            _employeeService.GetEmployeeByIdAsync(id);
        }

        private void ViewAllEmployees()
        {
            _employeeService.GetAllEmployeesAsync();
        }

        private void DeleteEmployee()
        {
            Console.WriteLine("Enter employee id: ");
            int id = int.Parse(Console.ReadLine());
            _employeeService.DeleteEmployeeAsync(id);
        }

        private void ListAllManagers()
        {
            _employeeService.GetAllManagersAsync();
        }

        private void EmployeeAverageAmount()
        {
            Console.WriteLine("Enter employee id: ");
            int id = int.Parse(Console.ReadLine());
            _employeeService.CalculateAverageOrderAmount(id);
        }
    }
}
