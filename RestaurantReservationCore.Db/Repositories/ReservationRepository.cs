using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private readonly RestaurantReservationDbContext _context;
        public ReservationRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
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
            return await _context.Reservations.Where(r => r.CustomerId == customerId).ToListAsync();
        }
    }
}
