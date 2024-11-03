using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Views;

namespace RestaurantReservationCore.Db.Repositories.ReservationManagement
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public ReservationRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.AsNoTracking().ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.AsNoTracking().FirstOrDefaultAsync(r => r.ReservationId == id);
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Reservation>> GetReservationsByCustomerIdAsync(int customerId)
        {
            return await _context.Reservations.AsNoTracking().Where(r => r.CustomerId == customerId).ToListAsync();
        }

        public async Task<List<CustomerReservationsByRestaurant>> GetCustomerReservationsByRestaurants()
        {
            return await _context.CustomerReservationsByRestaurants.AsNoTracking().ToListAsync();
        }
    }
}