using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;

namespace RestaurantReservationCore.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task GetAllEmployeesAsync()
        {
            List<Employee> employees = await _employeeRepository.GetAllAsync();
            if (!employees.Any())
            {
                Console.WriteLine("No employees found");
                return;
            }
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
        }

        public async Task GetEmployeeByIdAsync(int id)
        {
            Employee employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                Console.WriteLine("Employee not found");
                return;
            }
            Console.WriteLine(employee);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            Employee requestedEmployee1 = await _employeeRepository.GetByIdAsync(employee.EmployeeId);
            if (requestedEmployee1 != null)
            {
                Console.WriteLine("Employee already exists");
                return;
            }
            await _employeeRepository.AddAsync(employee);
        }

        public async Task UpdateEmployeeAsync(int id, Employee employee)
        {
            Employee updatedEmployee = await _employeeRepository.GetByIdAsync(id);
            if (updatedEmployee == null)
            {
                Console.WriteLine("Employee not found");
                return;
            }
            updatedEmployee.FirstName = employee.FirstName;
            updatedEmployee.LastName = employee.LastName;
            updatedEmployee.Position = employee.Position;
            updatedEmployee.RestaurantId = employee.RestaurantId;
            await _employeeRepository.UpdateAsync(updatedEmployee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                Console.WriteLine("Employee doesn't exist");
                return;
            }
            await _employeeRepository.DeleteAsync(employee);
        }

        public async Task GetAllManagersAsync()
        {
            List<Employee> employees = await _employeeRepository.ListAllManagers();
            if (!employees.Any())
            {
                Console.WriteLine("No managers found");
                return;
            }
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
        }

        public async Task CalculateAverageOrderAmountAsync(int employeeId)
        {
            double totalAmount = await _employeeRepository.GetEmployeeTotalAmountAsync(employeeId);
            int numberOfOrders = await _employeeRepository.GetEmployeeNumberOfOrdersAsync(employeeId);
            Console.WriteLine(totalAmount / numberOfOrders);
        }
    }
}