using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly RestaurantReservationDbContext _context;
        public OrderItemRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(o => o.OrderItemId == id);
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
