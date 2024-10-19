using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public EmployeeRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> ListAllManagers()
        {
            return await _context.Employees.Where(e => e.Position == "Manager").ToListAsync();
        }

        public async Task<double> GetEmployeeTotalAmountAsync(int employeeId)
        {
            return await _context.Orders.Where(e => e.EmployeeId == employeeId).SumAsync(o => o.TotalAmount);
        }

        public async Task<int> GetEmployeeNumberOfOrdersAsync(int employeeId)
        {
            return await _context.Orders.Where(e => e.EmployeeId == employeeId).CountAsync();
        }
    }
}