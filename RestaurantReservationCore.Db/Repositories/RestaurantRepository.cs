using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories
{
    public class RestaurantRepository : IRepository<Restaurant>
    {
        private readonly RestaurantReservationDbContext _context;
        public RestaurantRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId == id);
        }

        public async Task AddAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
