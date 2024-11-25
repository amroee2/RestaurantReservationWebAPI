using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationServices.Services.EmployeeManagementService
{
    public interface IEmployeeService
    {
        Task GetAllEmployeesAsync();
        Task GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(int employeeId, Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task GetAllManagersAsync();
        Task CalculateAverageOrderAmountAsync(int employeeId);
    }
}
