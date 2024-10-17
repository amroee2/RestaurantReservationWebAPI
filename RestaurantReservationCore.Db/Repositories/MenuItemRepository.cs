using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories
{
    public class MenuItemRepository : IRepository<MenuItem>
    {

        private readonly RestaurantReservationDbContext _context;
        public MenuItemRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems.ToListAsync();
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == id);
        }

        public async Task AddAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}
