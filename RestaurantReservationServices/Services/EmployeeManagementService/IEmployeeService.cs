using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.EmployeeDTOs;

namespace RestaurantReservationServices.Services.EmployeeManagementService
{
    public interface IEmployeeService
    {
        Task<List<EmployeeReadDTO>> GetAllEmployeesAsync();
        Task<EmployeeReadDTO> GetEmployeeByIdAsync(int id);
        Task<int> AddEmployeeAsync(EmployeeCreateDTO employee);
        Task UpdateEmployeeAsync(int employeeId, EmployeeUpdateDTO employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task<List<EmployeeReadDTO>> GetAllManagersAsync();
        Task<double> CalculateAverageOrderAmountAsync(int employeeId);
    }
}
