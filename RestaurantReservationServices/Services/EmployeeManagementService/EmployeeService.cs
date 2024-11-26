using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.EmployeeManagement;
using RestaurantReservationServices.DTOs.EmployeeDTOs;
using RestaurantReservationServices.Exceptions;

namespace RestaurantReservationServices.Services.EmployeeManagementService
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<List<EmployeeReadDTO>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<List<EmployeeReadDTO>>(employees);
        }

        public async Task<EmployeeReadDTO> GetEmployeeByIdAsync(int id)
        {
            Employee employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new EntityNotFoundException("Employee not found");
            }
            return _mapper.Map<EmployeeReadDTO>(employee);
        }

        public async Task<int> AddEmployeeAsync(EmployeeCreateDTO employee)
        {
            var newEmployee = _mapper.Map<Employee>(employee);
            await _employeeRepository.AddAsync(newEmployee);
            return newEmployee.EmployeeId;
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeUpdateDTO employee)
        {
            Employee updatedEmployee = await _employeeRepository.GetByIdAsync(id);
            _mapper.Map(employee, updatedEmployee);
            await _employeeRepository.UpdateAsync(updatedEmployee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            await _employeeRepository.DeleteAsync(employee);
        }

        public async Task<List<EmployeeReadDTO>> GetAllManagersAsync()
        {
            List<Employee> employees = await _employeeRepository.ListAllManagersAsync();
            return _mapper.Map<List<EmployeeReadDTO>>(employees);
        }

        public async Task<double> CalculateAverageOrderAmountAsync(int employeeId)
        {
            double totalAmount = await _employeeRepository.GetEmployeeTotalAmountAsync(employeeId);
            int numberOfOrders = await _employeeRepository.GetEmployeeNumberOfOrdersAsync(employeeId);
            return totalAmount / numberOfOrders;
        }
    }
}