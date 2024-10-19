using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories.EmployeeManagement
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<List<Employee>> ListAllManagers();

        Task<double> GetEmployeeTotalAmountAsync(int employeeId);

        Task<int> GetEmployeeNumberOfOrdersAsync(int employeeId);
    }
}