using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<List<Employee>> ListAllManagers();
        Task<double> EmployeeTotalAmount(int employeeId);
        Task<int> NumberOfOrders(int employeeId);
    }
}
