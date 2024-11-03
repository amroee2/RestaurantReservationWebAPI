using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories.CustomerManagement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public CustomerRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetCustomersWithBigPartySizeAsync(int partySize)
        {
            var customers = await _context.Customers.FromSqlRaw("CustomersWithBigPartySize {0}", partySize).ToListAsync();
            return customers;
        }
    }
}